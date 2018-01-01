using LaptopMart.ViewModels;
using System.Collections.Generic;

namespace LaptopMart.Models
{
    public class Category
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public string Description { get; set; }

        public void SetObject(CategoryFormViewModel viewModel)
        {
            Id = viewModel.Id;
            Name = viewModel.Name;
            Description = viewModel.Description;
        }
    }
}