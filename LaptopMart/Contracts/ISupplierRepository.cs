using System.Collections.Generic;
using LaptopMart.Models;

namespace LaptopMart.Contracts
{
    public interface ISupplierRepository
    {
        void Create(Supplier supplier);
        Supplier Delete(int id);
        ApplicationUser DeleteSupplierByAdmin(int supplierId);
        Supplier Read(int id);
        IEnumerable<Supplier> ReadAll();
        void Update(Supplier supplier);
    }
}