using LaptopMart.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LaptopMart.ViewModels
{
    public class ProductFormViewModel
    {
        public ProductFormViewModel()
        {
            CategoryIds = new List<int>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        [DisplayName("Stock")]
        public int? StockQuantity { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name="Category")]
        public ICollection<int> CategoryIds { get; set; }

        public ICollection<string> CategoryNames { get; set; }
        
        public string Image { get; set; }

        public IEnumerable<Category> CategoriesDropDownList { get; set; }
       
    }
}