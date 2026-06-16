using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowMart.Models
{
    public class Sale
    {
        [Key]
        public Guid SaleId { get; set; }

        [Required, StringLength(50)]
        public string VoucherNumber { get; set; }

        [Required]
        public Guid StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        [ForeignKey(nameof(StaffId))]
        public Staff Staff { get; set; }

        [Required]
        public Guid ShiftId { get; set; }

        [ForeignKey(nameof(ShiftId))]
        public CashShift CashShift { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }

        [Required, StringLength(50)]
        public string PaymentMethod { get; set; }

        [Required, StringLength(50)]
        public string PaymentStatus { get; set; }

        // Navigation
        public ICollection<SaleItem> SaleItems { get; set; }
    }
}
