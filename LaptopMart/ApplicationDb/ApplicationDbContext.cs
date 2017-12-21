using LaptopMart.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LaptopMart.ApplicationDb
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Product> ProductTable { get; set; }

        public DbSet<Supplier> SupplierTable { get; set; }

        public DbSet<Category> CategoryTable { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("ProductTable");
            modelBuilder.Entity<Category>().ToTable("CategoryTable");
            modelBuilder.Entity<Supplier>().ToTable("SupplierTable");

            modelBuilder.Entity<Product>().HasRequired<Category>(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey<int>(p => p.CategoryId);

            modelBuilder.Entity<Product>().HasRequired<Supplier>(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey<int>(p => p.SupplierId);

            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Supplier>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            

            base.OnModelCreating(modelBuilder);
        }
    }
}