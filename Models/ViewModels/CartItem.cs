//namespace GlowMart.Models.ViewModels
//{
//    public class CartItem
//    {
//        public string Barcode { get; set; }
//        public string ProductName { get; set; }

//        public decimal SalePrice { get; set; }
//        public int Quantity { get; set; }
//        public decimal TotalPrice { get; set; }
//        //this is I add new cloum
//        public int DiscountPercent { get; set; } = 0;
//        public DateTime? ExpiredAt { get; set; }


//        //this is member only 
//        public string MemberLevelName { get; set; } = "No Member";
//        public int MemberDiscountPercent { get; set; } = 0;
//    }
//}
namespace GlowMart.Models.ViewModels
{
    public class CartItem
    {
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }          // base price
        public int DiscountPercent { get; set; }        // expiry + member combined

        // Calculated properties
        public decimal Subtotal => Quantity * SalePrice;
        public decimal DiscountAmount => Subtotal * (DiscountPercent / 100m);
        public decimal TotalPrice => Subtotal - DiscountAmount;
    }
}
