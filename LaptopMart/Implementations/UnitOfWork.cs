using LaptopMart.ApplicationDb;
using LaptopMart.Contracts;

namespace LaptopMart.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
      



        private readonly ApplicationDbContext _context;

        

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(context);
            CategoryRepository = new CategoryRepository(context);
            SupplierRepository = new SupplierRepository(context);
            ApplicationUserRepository = new ApplicationUserRepository(context);
            CartRepository = new CartRepository(context);
            CartItemRepository = new CartItemRepository(context);
        }


        public IProductRepository ProductRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public ISupplierRepository SupplierRepository { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }
        public ICartRepository CartRepository { get; private set; }
        public ICartItemRepository CartItemRepository { get; private set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
        /*
                public IRepository<TEntity> Repository<TEntity>() where TEntity : class
                {
                    if (_repositories == null)
                        _repositories = new Hashtable();

                    var type = typeof(TEntity).Name;

                    if (_repositories.ContainsKey(type)) return (IRepository<TEntity>)_repositories[type];

                    var repositoryType = typeof(Repository<>);

                    var repositoryInstance =
                        Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(TEntity)), _context);

                    _repositories.Add(type, repositoryInstance);

                    return (IRepository<TEntity>)_repositories[type];
                }*/
       


    }


}
