using LaptopMart.ApplicationDb;
using LaptopMart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LaptopMart.Implementations
{
    public class SupplierRepository
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

        public IEnumerable<Product> ReadAllProductsBySupplier(string supplierName)
        {
            if (typeof(T) != typeof(Product))
            {
                throw new Exception("T must be of type Product");
            }

            var productsFromSupplier = _context.ProductTable.Where(p => p.SupplierName.Equals(supplierName));
            return productsFromSupplier;
        }

        public Product ReadProductBySupplier(string supplierName, int productId)
        {
            if (typeof(T) != typeof(Product))
            {
                throw new Exception("T must be of type Product");
            }
            var productFromSupplier = _context.ProductTable.SingleOrDefault(p => p.SupplierName.Equals(supplierName) && p.Id == productId);
            return productFromSupplier;

        }

        public ApplicationUser DeleteSupplierByAdmin(int supplierId)
        {
            // var um = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (typeof(T) != typeof(Supplier))
            {
                throw new Exception("T must be of type supplier");
            }

            var supplierToBeSuspended = _context.Set<Supplier>().SingleOrDefault(s => s.Id == supplierId);
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