using LaptopMart.Models;
using System.Collections.Generic;

namespace LaptopMart.Contracts
{
    public interface ICategoryRepository
    {
        void Create(Category category);
        Category Delete(int id);
        Category Read(int id);
        
        IEnumerable<Category> ReadSpecificCategories(int parentId);
        IEnumerable<Category> ReadAll();
        void Update(Category category);
    }
}