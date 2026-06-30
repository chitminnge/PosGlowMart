using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowMart.Models
{
    public class Membership
    {
        [Key]
        public Guid MemberId { get; set; }

        public string MemberName { get; set; }

        public string Phone { get; set; }

        public string? Address { get; set; }

        public string CardNumber { get; set; }

        public int Point { get; set; } = 0;

        public Guid LevelId { get; set; }

        [ForeignKey(nameof(LevelId))]
        public Level? Level { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiredAt { get; set; }
    }
}