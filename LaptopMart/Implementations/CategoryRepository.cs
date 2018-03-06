using LaptopMart.ApplicationDb;
using LaptopMart.Contracts;
using LaptopMart.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LaptopMart.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Category category)
        {
            _context.CategoryTable.Add(category);
        }

        public IEnumerable<Category> ReadAll()
        {
            return _context.CategoryTable.ToList();
        }

        public Category Read(int id)
        {
            return _context.CategoryTable.Find(id);
        }

        public void Update(Category category)
        {
            _context.CategoryTable.Attach(category);
            _context.Entry(category).State = EntityState.Modified;
        }

        public Category Delete(int id)
        {
            var category = Read(id);
            if (category == null)
            {
                return null;
            }
            if (_context.Entry(category).State == EntityState.Detached)
                _context.CategoryTable.Attach(category);

            _context.CategoryTable.Remove(category);
            return category;

        }


        public IEnumerable<Category> ReadSpecificCategories(int parentId)
        {
            return _context.CategoryTable.Where(p => p.ParentId == parentId);
        }
    }
}