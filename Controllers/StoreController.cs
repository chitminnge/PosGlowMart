using GlowMart.Models;
using GlowMart.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlowMart.Controllers
{
  

    public class StoreController : Controller
    {
        private readonly AppDbContext _db;
        public StoreController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddStore()
        {
            return View();
        }
        [HttpPost]
    
        public async Task<IActionResult> AddStore(StoreViewModel storeviewmodel)
        {
            if (ModelState.IsValid)
            {
                var store = new Store
                {
                    StoreId = Guid.NewGuid(),
                    StoreName = storeviewmodel.StoreName,
                    Address = storeviewmodel.Address,
                    Phone = storeviewmodel.Phone,
                    CreatedAt = DateTime.Now
                };

                _db.Stores.Add(store);
                await _db.SaveChangesAsync();
                return RedirectToAction("StoreList");
            }
            return View(storeviewmodel); // still returning ViewModel
        }

        public IActionResult StoreList()
        {
            return View();
        }

    }
}
