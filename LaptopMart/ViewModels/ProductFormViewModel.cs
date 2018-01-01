using LaptopMart.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LaptopMart.ViewModels
{
    public class ProductFormViewModel
    {

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
        public string CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}