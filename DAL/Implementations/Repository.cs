using LaptopMart.ApplicationDbContext;
using LaptopMart.Contracts;
using System.Collections.Generic;
using System.Data.Entity;

namespace DAL.Implementations
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
            return _context.Set<T>();
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

        public void Delete(int id)
        {
            var t = Read(id);
            if (_context.Entry(t).State == EntityState.Detached)
                _context.Set<T>().Attach(t);

            _context.Set<T>().Remove(t);

        }
    }
}
