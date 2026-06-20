using GlowMart.Models;
using GlowMart.Models.ViewModels;
using GlowMart.PasswordEnDe;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace GlowMart.Controllers
{
    public class StaffController : Controller
    {
        private readonly AppDbContext _db;
        public StaffController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddStaff()
        {
            ViewBag.Stores = _db.Stores.ToList();
            return View();
        }

        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> AddStaff(StaffViewModel staffViewModel) // string password ကို ဖယ်လိုက်ပါ
        {
            if (!ModelState.IsValid)
            {
                // staffViewModel.Password မှ တန်ဖိုးကို တိုက်ရိုက်ယူသုံးပါသည်
                var (hash, salt) = PasswordHelper.HashPassword(staffViewModel.Password);

                var staff = new Staff
                {
                    StaffId = Guid.NewGuid(),
                    StoreId = staffViewModel.StoreId,
                    UserName = staffViewModel.UserName,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    RoleName = staffViewModel.RoleName,
                    AccountStatus = "Active",
                    CreatedAt = DateTime.Now
                };

                _db.Staffs.Add(staff);
                await _db.SaveChangesAsync();
                return RedirectToAction("StaffList");
            }

            // Validation မအောင်မြင်ရင် Dropdown List ပြန်ပွင့်ဖို့ Stores ကို ပြန်ရှာပေးရမည်
            ViewBag.Stores = _db.Stores.ToList();
            return View(staffViewModel);
        }
        public IActionResult StaffList()
        {
            var staffs = _db.Staffs.ToList();
            return View(staffs);
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(
            string username,
            string oldPassword,
            string newPassword)
        {
            try
            {
                var staff = _db.Staffs
                    .FirstOrDefault(s => s.UserName == username);

                if (staff == null)
                {
                    ViewBag.Error = "User not found!";
                    return View();
                }

                bool isValid = PasswordHelper.VerifyPassword(
                    oldPassword,
                    staff.PasswordHash,
                    staff.PasswordSalt);

                if (!isValid)
                {
                    ViewBag.Error = "Old password incorrect!";
                    return View();
                }

                var (hash, salt) =
                    PasswordHelper.HashPassword(newPassword);

                staff.PasswordHash = hash;
                staff.PasswordSalt = salt;

                await _db.SaveChangesAsync();

                ViewBag.Message = "Password changed successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }


    }
}


