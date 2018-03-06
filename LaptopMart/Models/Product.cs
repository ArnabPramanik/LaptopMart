using System.Collections.Generic;

namespace LaptopMart.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string Image { get; set; }

        public virtual ISet<Category> Categories { get; set; }

       

        public Supplier Supplier { get; set; }
        public string SupplierName { get; set; }


        public void Update(Product product)
        {
            Name = product.Name;
            Description = product.Description;
            Price = (decimal) product.Price;
            StockQuantity = (int) product.StockQuantity;
            Image = product.Image;
            SupplierName = product.SupplierName;
            foreach(Category category in product.Categories)
            {
                Categories.Add(category);
            }


        }
    }
}