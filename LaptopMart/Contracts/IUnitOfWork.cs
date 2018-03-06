namespace LaptopMart.Contracts
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        ICartRepository CartRepository { get; }
        ICartItemRepository CartItemRepository { get; }

        void Complete();
        
    }
}