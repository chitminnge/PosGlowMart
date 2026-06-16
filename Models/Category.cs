using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace GlowMart.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required, StringLength(100)]
        public string CategoryName { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Product> Products { get; set; }
    }
}
