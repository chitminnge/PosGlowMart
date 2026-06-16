using System.ComponentModel.DataAnnotations;

namespace GlowMart.Models
{
    public class Store
    {
        [Key]
        public Guid StoreId { get; set; }

        [Required, StringLength(100)]
        public string StoreName { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<Staff> Staffs { get; set; }
        public ICollection<CashShift> CashShifts { get; set; }
        public ICollection<StoreStock> StoreStocks { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
