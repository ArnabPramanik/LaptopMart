using LaptopMart.Models;
using System.Collections.Generic;

namespace LaptopMart.Contracts
{
    public interface IProductRepository
    {
        void Create(Product product);
        Product Delete(int id);
        ApplicationUser DeleteSupplierByAdmin(int supplierId);
        Product Read(int id);
        Product ReadLast();
        IEnumerable<Product> ReadAll();
        IEnumerable<Product> ReadAllProductsBySupplier(string supplierName);
        Product ReadProductBySupplier(string supplierName, int productId);
        void Update(Product product);
    }
}