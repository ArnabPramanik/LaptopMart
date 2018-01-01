using LaptopMart.ApplicationDb;
using LaptopMart.Contracts;
using LaptopMart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LaptopMart.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        

        public Repository(ApplicationDbContext context)
        {
            _context = context;

        }
        public void Create(T t)
        {
            _context.Set<T>().Add(t);
        }

        public IEnumerable<T> ReadAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Read(int id)
        {
           return _context.Set<T>().Find(id);
        }

        public void Update(T t)
        {
            _context.Set<T>().Attach(t);
            _context.Entry(t).State = EntityState.Modified;
        }

        public T Delete(int id)
        {
            var t = Read(id);
            if (t == null)
            {
                return null;
            }
            if (_context.Entry(t).State == EntityState.Detached)
                _context.Set<T>().Attach(t);

            _context.Set<T>().Remove(t);
            return t;

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
