using System.ComponentModel.DataAnnotations;

namespace LaptopMart.ViewModels
{
    public class BecomeASupplierViewModel
    {
        [Display(Name="Brand")]
        [Required]
        [MaxLength(50)]
        public string BrandName { get; set; }

        [Display(Name = "Brand Description")]
        [MaxLength(300)]
        public string Description { get; set; }
    }
}