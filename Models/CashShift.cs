using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowMart.Models
{
    public class CashShift
    {
        [Key]
        public Guid ShiftId { get; set; }

        [Required]
        public Guid StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        [ForeignKey(nameof(StaffId))]
        public Staff Staff { get; set; }

        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OpeningBalance { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ClosingBalance { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ActualSalesAmount { get; set; } = 0;

        [StringLength(20)]
        public string Status { get; set; } = "Open";

        // Navigation
        public ICollection<Sale> Sales { get; set; }
    }
}
