
using GlowMart.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlowMart.Controllers
{
    [Route("CashierUI/[action]")]
    public class CashierUIController : Controller
    {
        private readonly AppDbContext _context;

        public CashierUIController(AppDbContext context)
        {
            _context = context;
        }
        //[HttpGet]
        //public IActionResult Dashboard()
        //{
        //    var openShift = _context.CashShifts.FirstOrDefault(s => s.Status == "Open");
        //    if (openShift != null)
        //    {
        //        ViewBag.ShiftId = openShift.ShiftId;
        //        ViewBag.OpeningBalance = openShift.OpeningBalance;
        //        ViewBag.ActualSalesAmount = openShift.ActualSalesAmount;
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CloseShift([FromBody] CloseShiftViewModel vm)
        //{
        //    if (!ModelState.IsValid)
        //        return Json(new { success = false, message = "Invalid data." });

        //    var shift = _context.CashShifts.Find(vm.ShiftId);
        //    if (shift == null || shift.Status != "Open")
        //        return Json(new { success = false, message = "Shift not found or already closed." });

        //    shift.ClosingBalance = vm.ClosingBalance;
        //    shift.EndTime = DateTime.Now;
        //    shift.ActualSalesAmount = shift.ClosingBalance - shift.OpeningBalance;
        //    shift.Status = "Closed";

        //    _context.CashShifts.Update(shift);
        //    _context.SaveChanges();

        //    return Json(new
        //    {
        //        success = true,
        //        message = "Shift closed successfully!",
        //        actualSales = shift.ActualSalesAmount
        //    });
        //}


        [HttpGet]
        public IActionResult Dashboard()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart")
                       ?? new List<CartItem>();
            return View(cart);
        }

        [HttpGet]
        public IActionResult LookupBarcode(string code)
        {
            var variant = _context.ProductVariants
                .Include(v => v.Product)
                .FirstOrDefault(v => v.Barcode == code && v.IsActive);

            if (variant == null)
                return Json(new { success = false });

            int expiryDiscount = 0;
            if (variant.ExpiredAt != null && variant.ExpiredAt <= DateTime.Now.AddMonths(3))
            {
                expiryDiscount = 5;
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart")
                       ?? new List<CartItem>();
            var existing = cart.FirstOrDefault(c => c.Barcode == variant.Barcode);

            if (existing != null)
            {
                existing.Quantity += 1;
                existing.DiscountPercent = expiryDiscount;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Barcode = variant.Barcode,
                    ProductName = variant.Product.ProductName + " - " + variant.VariantName,
                    SalePrice = variant.SalePrice,
                    Quantity = 1,
                    DiscountPercent = expiryDiscount
                });
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Json(new
            {
                success = true,
                product = new
                {
                    barcode = variant.Barcode,
                    productName = variant.Product.ProductName + " - " + variant.VariantName,
                    salePrice = variant.SalePrice,
                    discountPercent = expiryDiscount,
                    expiredAt = variant.ExpiredAt
                }
            });
        }

        [HttpGet]
        public IActionResult LookupMember(string cardNumber)
        {
            var member = _context.Memberships
                .Include(m => m.Level)
                .FirstOrDefault(m => m.CardNumber == cardNumber && m.IsActive);

            if (member == null)
                return Json(new { success = false, message = "Member not found" });

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart")
                       ?? new List<CartItem>();

            foreach (var item in cart)
            {
                var memberDiscount = member.Level?.DiscountPercent ?? 0;

                // Multiplicative stacking
                decimal expiryFactor = 1 - (item.DiscountPercent / 100m);
                decimal memberFactor = 1 - (memberDiscount / 100m);
                decimal combinedFactor = expiryFactor * memberFactor;

                item.DiscountPercent = (int)((1 - combinedFactor) * 100);
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Json(new
            {
                success = true,
                member = new
                {
                    memberName = member.MemberName,
                    cardNumber = member.CardNumber,
                    levelName = member.Level?.LevelName ?? "No Member",
                    discountPercent = member.Level?.DiscountPercent ?? 0
                }
            });





        }

        [HttpPost]
        public IActionResult RemoveItem([FromBody] string barcode)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart")
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(c => c.Barcode == barcode);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            // ✅ GrandTotal ကို အမြဲပြန်တွက်
            var grandTotal = cart.Sum(c => c.TotalPrice);

            // ✅ JSON response ပြန်ပေး
            return Json(new { success = true, grandTotal });
        }





    }


}

