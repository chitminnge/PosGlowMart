using System;
using System.ComponentModel.DataAnnotations;

namespace GlowMart.Models.ViewModels
{
    public class CashShiftViewModel
    {
        public Guid ShiftId { get; set; }

        [Required]
        public Guid StoreId { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        [Display(Name = "Staff Name")]
        public string StaffName { get; set; } = string.Empty;

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        [Required]
        [Display(Name = "Opening Balance")]
        [Range(0, 999999.99, ErrorMessage = "Opening balance must be positive.")]
        public decimal OpeningBalance { get; set; }

        [Display(Name = "Closing Balance")]
        public decimal ClosingBalance { get; set; }

        [Display(Name = "Actual Sales Amount")]
        public decimal ActualSalesAmount { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Open";
    }
}
