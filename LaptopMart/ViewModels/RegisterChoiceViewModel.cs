using System.ComponentModel.DataAnnotations;

namespace LaptopMart.ViewModels
{
    public class RegisterChoiceViewModel
    {
        [Required]
        [Display(Name="User Role")]
        public string UserRole { get; set; }
    }
}