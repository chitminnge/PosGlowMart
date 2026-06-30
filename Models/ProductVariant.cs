using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowMart.Models
{
    public class ProductVariant
    {
        [Key]
        public Guid VariantId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required, StringLength(50)]
        public string Barcode { get; set; }

        [Required, StringLength(100)]
        public string VariantName { get; set; }

        [Required, StringLength(200)]
        public string VariantImage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [NotMapped]
        public decimal Profit => SalePrice - CostPrice;

        public int MinStock { get; set; } = 5;

        public bool IsActive { get; set; } = true;

        //this is I add new cloum 
        public DateTime? ExpiredAt { get; set; }

        // Navigation
        public ICollection<StoreStock> StoreStocks { get; set; }

        public ICollection<SaleItem> SaleItems { get; set; }
    }
}