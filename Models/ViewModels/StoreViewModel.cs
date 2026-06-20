using System.ComponentModel.DataAnnotations;

namespace GlowMart.Models.ViewModels
{
    public class StoreViewModel
    {
       
            [Required(ErrorMessage = "Store name is required")]
            [StringLength(100, ErrorMessage = "Store name cannot exceed 100 characters")]
            public string StoreName { get; set; }=string.Empty;

            [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters")]
            public string Address { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Phone cannot exceed 50 characters")]
            public string Phone { get; set; } = string.Empty;

    }
}
