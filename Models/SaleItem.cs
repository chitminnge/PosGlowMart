using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowMart.Models
{
    public class SaleItem
    {
        [Key]
        public Guid SaleItemId { get; set; }

        [Required]
        public Guid SaleId { get; set; }

        [ForeignKey(nameof(SaleId))]
        public Sale Sale { get; set; }

        [Required]
        public Guid StockId { get; set; }

        [ForeignKey(nameof(StockId))]
        public StoreStock StoreStock { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
