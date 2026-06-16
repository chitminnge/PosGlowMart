using Microsoft.AspNetCore.Mvc;

namespace GlowMart.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View(); // looks for Views/Admin/Login.cshtml
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            // TODO: validate credentials against database
            if (Email == "admin@gmail.com" && Password == "12345")
            {
                return RedirectToAction("Dashboard");
            }
            ViewBag.Error = "Invalid login";
            return View();
        }

        public IActionResult Dashboard()
        {
            return View(); // Views/Admin/Dashboard.cshtml
        }
    }
}
