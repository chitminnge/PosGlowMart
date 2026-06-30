using System.ComponentModel.DataAnnotations;

namespace GlowMart.Models
{
    public class Level
    {
        [Key]
        public Guid LevelId { get; set; }

        public string LevelName { get; set; }

        public int RequiredPoint { get; set; }

        public decimal DiscountPercent { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Property
        public ICollection<Membership> Memberships { get; set; }
            = new List<Membership>();
    }
}