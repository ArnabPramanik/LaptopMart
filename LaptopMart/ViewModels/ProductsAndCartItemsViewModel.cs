using LaptopMart.Models;
using System.Collections.Generic;

namespace LaptopMart.ViewModels
{
    public class ProductsAndCartItemsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<CartItemViewModel> CartItems { get; set; }
    }
}