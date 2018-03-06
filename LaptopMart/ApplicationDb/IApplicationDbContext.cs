using LaptopMart.Models;
using System.Data.Entity;

namespace LaptopMart.ApplicationDb
{
    public interface IApplicationDbContext
    {
        DbSet<Category> CategoryTable { get; set; }
        DbSet<Product> ProductTable { get; set; }
        DbSet<Supplier> SupplierTable { get; set; }
        DbSet<Cart> CartTable { get; set; }
        DbSet<CartItem> CartItemTable { get; set; }
    }
}