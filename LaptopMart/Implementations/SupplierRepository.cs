using LaptopMart.ApplicationDb;
using LaptopMart.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LaptopMart.Contracts;

namespace LaptopMart.Implementations
{
    public class SupplierRepository : ISupplierRepository
    {

        private readonly ApplicationDbContext _context;


        public  SupplierRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public void Create(Supplier supplier)
        {
            _context.SupplierTable.Add(supplier);
        }

        public IEnumerable<Supplier> ReadAll()
        {
            return _context.SupplierTable.ToList();
        }

        public Supplier Read(int id)
        {
            return _context.SupplierTable.Find(id);
        }

        public void Update(Supplier supplier)
        {
            _context.SupplierTable.Attach(supplier);
            _context.Entry(supplier).State = EntityState.Modified;
        }

        public Supplier Delete(int id)
        {
            var supplier = Read(id);
            if (supplier == null)
            {
                return null;
            }
            if (_context.Entry(supplier).State == EntityState.Detached)
                _context.SupplierTable.Attach(supplier);

            _context.SupplierTable.Remove(supplier);
            return supplier;

        }


        public ApplicationUser DeleteSupplierByAdmin(int supplierId)
        {
            // var um = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
           

            var supplierToBeSuspended = _context.SupplierTable.SingleOrDefault(s => s.Id == supplierId);
            if (supplierToBeSuspended == null)
            {
                return null;
            }
            var userSupplier = _context.Users.SingleOrDefault(u => u.Name.Equals(supplierToBeSuspended.Name));
            if (userSupplier == null)
            {
                return null;
            }

            _context.Users.Remove(userSupplier);
            _context.SupplierTable.Remove(supplierToBeSuspended);

            return userSupplier;

        }
    }
}