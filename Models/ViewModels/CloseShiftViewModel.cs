using System;
using System.ComponentModel.DataAnnotations;

namespace GlowMart.Models.ViewModels
{
    public class CloseShiftViewModel
    {
        [Required]
        public Guid ShiftId { get; set; }

        [Required]
        [Range(0, 999999.99, ErrorMessage = "Closing balance must be positive.")]
        public decimal ClosingBalance { get; set; }

        public decimal ActualSalesAmount { get; set; }
    }
}
