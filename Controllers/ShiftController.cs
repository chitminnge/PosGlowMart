using GlowMart.Models;
using GlowMart.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;
using System;

namespace GlowMart.Controllers
{
    public class ShiftController : Controller
    {
        private readonly AppDbContext _context;

        public ShiftController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Opening form
        [HttpGet]
        public IActionResult Opening()
        {
            var staffId = HttpContext.Session.GetString("StaffId");
            var storeId = HttpContext.Session.GetString("StoreId");

            if (string.IsNullOrEmpty(staffId) || string.IsNullOrEmpty(storeId))
            {
                TempData["Error"] = "Session expired. Please login again.";
                return RedirectToAction("Login", "Cashier");
            }

            var vm = new CashShiftViewModel
            {
                StaffId = Guid.Parse(staffId),
                StoreId = Guid.Parse(storeId),
                StartTime = DateTime.Now
            };

            return View(vm);
        }

        // POST: Opening form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Opening(CashShiftViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var shift = new CashShift
            {
                ShiftId = Guid.NewGuid(),
                StoreId = vm.StoreId,
                StaffId = vm.StaffId,
                StartTime = DateTime.Now,
                OpeningBalance = vm.OpeningBalance,
                Status = "Open"
            };

            _context.CashShifts.Add(shift);
            _context.SaveChanges();

            TempData["Success"] = "Shift opened successfully!";
            return RedirectToAction("Dashboard", "CashierUI");
        }
    }
}
