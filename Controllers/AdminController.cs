using GlowMart.Models.ViewModels;
using GlowMart.PasswordEnDe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlowMart.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // looks for Views/Admin/Login.cshtml
        }

        [HttpPost]

        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var staff = _context.Staffs.FirstOrDefault(
     s => s.UserName == vm.UserName
       && s.IsActive
       && s.AccountStatus == "Active"
 );

            if (staff == null)
            {
                TempData["Error"] = "Invalid username.";
                return View(vm);
            }

            // Verify password
            bool valid = PasswordHelper.VerifyPassword(vm.Password, staff.PasswordHash, staff.PasswordSalt);
            if (!valid)
            {
                TempData["Error"] = "Invalid password.";
                return View(vm);
            }

            // Save session
            HttpContext.Session.SetString("StaffId", staff.StaffId.ToString());
            HttpContext.Session.SetString("RoleName", staff.RoleName);

            // Redirect based on role
            if (staff.RoleName == "Cashier")
                return RedirectToAction("opening", "Shift");
            else if (staff.RoleName == "Admin")
                return RedirectToAction("Dashboard", "AdminUI");
            else
                return RedirectToAction("Index", "Home");
        }



        public IActionResult Dashboard()
        {
            return View(); // Views/Admin/Dashboard.cshtml
        }
    }
}
