using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowMart.Models
{
    public class Staff
    {
        [Key]
        public Guid StaffId { get; set; }

        [Required]
        public Guid StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }

        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; }

        [Required, StringLength(255)]
        public string PasswordHash { get; set; }

        [Required, StringLength(225)]
        public string PasswordSalt { get; set; }

        public string AccountStatus { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<CashShift> CashShifts { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
