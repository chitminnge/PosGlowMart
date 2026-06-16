using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowMart.Models
{
    public class StoreStock
    {
        [Key]
        public Guid StockId { get; set; }

        [Required]
        public Guid StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }

        [Required]
        public Guid VariantId { get; set; }

        [ForeignKey(nameof(VariantId))]
        public ProductVariant Variant { get; set; }

        public int Quantity { get; set; }

        public DateTime? ExpiredDate { get; set; }

        // Navigation
        public ICollection<SaleItem> SaleItems { get; set; }
    }
}
