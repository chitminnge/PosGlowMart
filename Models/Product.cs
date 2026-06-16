using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowMart.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Required, StringLength(100)]
        public string BrandName { get; set; }

        [Required, StringLength(150)]
        public string ProductName { get; set; }

        // Navigation
        public ICollection<ProductVariant> Variants { get; set; }
    }
}
