using LaptopMart.ApplicationDb;
using LaptopMart.Contracts;
using LaptopMart.Models;
using System;
using System.Collections;

namespace LaptopMart.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
       // private Hashtable _repositories;



        private readonly ApplicationDbContext _context;

        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public IRepository<Product> ProductRepository { get; private set; }
        public IRepository<Category> CategoryRepository { get; private set; }
        public IRepository<Supplier> SupplierRepository { get; private set; }

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
        }


    }


}
