using LaptopMart.Models;
using System.Collections.Generic;

namespace LaptopMart.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity t);


        IEnumerable<TEntity> ReadAll();


        TEntity Read(int id);


        void Update(TEntity t);


        TEntity Delete(int id);

        ApplicationUser DeleteSupplierByAdmin(int supplierId);

        IEnumerable<Product> ReadAllProductsBySupplier(string supplierName);

        Product ReadProductBySupplier(string supplierName, int productId);


    }
}
