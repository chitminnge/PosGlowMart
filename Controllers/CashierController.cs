using Microsoft.AspNetCore.Mvc;

namespace GlowMart.Controllers
{
    public class CashierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login() => View();
        [HttpPost]
        public IActionResult Login(string OtpCode)
        {
            if (OtpCode == "123456")
                return RedirectToAction("Dashboard");
            ViewBag.Error = "Invalid OTP";
            return View();
        }
        public IActionResult Dashboard() => View();
    }
}
