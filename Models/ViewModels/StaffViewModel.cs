using System.ComponentModel.DataAnnotations;

namespace GlowMart.Models.ViewModels
{
    public class StaffViewModel
    {
        [Required(ErrorMessage = "StoreId is required")]
        public Guid StoreId { get; set; }

        public Store Store { get; set; }

        [Required(ErrorMessage = "User name is required")]
        [StringLength(100, ErrorMessage = "User name cannot exceed 100 characters")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;// User input password (not stored directly)

        [Required(ErrorMessage = "Role name is required")]
        [StringLength(50, ErrorMessage = "Role name cannot exceed 50 characters")]
        public string RoleName { get; set; } = string.Empty; // Admin / Cashier

        [StringLength(20)]
        public string AccountStatus { get; set; } = "Active";
    }
}
