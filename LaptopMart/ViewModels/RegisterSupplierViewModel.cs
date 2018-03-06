using System.ComponentModel.DataAnnotations;

namespace LaptopMart.ViewModels
{
    public class RegisterSupplierViewModel
    {

        [Required]
        [Display(Name = "User Role")]
        public string UserRole { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Brand Name")]
        [MaxLength(50)]
        public string BrandName { get; set; }

        [Required]
        [Display(Name = "Your Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Display(Name = "Brand Description")]
        [MaxLength(300)]
        public string Description { get; set; }
    }
}