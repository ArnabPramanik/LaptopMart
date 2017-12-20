using LaptopMart.ApplicationDbContext;
using LaptopMart.Contracts;
using LaptopMart.Models;

namespace DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Product> ProductRepository { get; private set; }
        public IRepository<Category> CategoryRepository { get; private set; }
        public IRepository<Supplier> SupplierRepository { get; private set; }

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
