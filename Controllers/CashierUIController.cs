using Microsoft.AspNetCore.Mvc;

namespace GlowMart.Controllers
{
    public class CashierUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
