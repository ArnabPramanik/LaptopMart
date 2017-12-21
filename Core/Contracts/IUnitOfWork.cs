﻿namespace Core.Contracts
{
    public interface IUnitOfWork
    {


        IRepository<Product> ProductRepository { get; }

        IRepository<Category> CategoryRepository { get; }

        IRepository<Supplier> SupplierRepository { get; }

        void Complete();
    }
}