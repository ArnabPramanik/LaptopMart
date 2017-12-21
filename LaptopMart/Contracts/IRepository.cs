using System.Collections.Generic;

namespace LaptopMart.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity t);


        IEnumerable<TEntity> ReadAll();


        TEntity Read(int id);


        void Update(TEntity t);


        void Delete(int id);



    }
}
