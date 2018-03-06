using LaptopMart.ApplicationDb;
using LaptopMart.Contracts;
using LaptopMart.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LaptopMart.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;


        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public void Create(Product product)
        {
            _context.ProductTable.Add(product);
        }

        public IEnumerable<Product> ReadAll()
        {
            return _context.ProductTable.ToList();
        }

        public Product Read(int id)
        {
            return _context.ProductTable.Include(p => p.Supplier).SingleOrDefault(p  => p.Id == id);
        }

        public Product ReadLast()
        {
            return _context.ProductTable.ToList().LastOrDefault();
        }

        public void Update(Product product)
        {
            _context.ProductTable.Attach(product);
            
            _context.Entry(product).State = EntityState.Modified;
            
        }

        public Product Delete(int id)
        {
            var product = Read(id);
            if (product == null)
            {
                return null;
            }
            if (_context.Entry(product).State == EntityState.Detached)
                _context.ProductTable.Attach(product);

            _context.ProductTable.Remove(product);
            return product;

        }

        public IEnumerable<Product> ReadAllProductsBySupplier(string supplierName)
        {

            var productsFromSupplier = _context.ProductTable.Where(p => p.SupplierName.Equals(supplierName));
            return productsFromSupplier;
        }

        public Product ReadProductBySupplier(string supplierName, int productId)
        {

            var productFromSupplier = _context.ProductTable.SingleOrDefault(p => p.SupplierName.Equals(supplierName) && p.Id == productId);
            return productFromSupplier;

        }

        public ApplicationUser DeleteSupplierByAdmin(int supplierId)
        {
            // var um = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();


            var supplierToBeSuspended = _context.Set<Product>().SingleOrDefault(s => s.Id == supplierId);
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
            _context.ProductTable.Remove(supplierToBeSuspended);

            return userSupplier;

        }

    }
}