using System.ComponentModel.DataAnnotations;

namespace LaptopMart.ViewModels
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

    }
}