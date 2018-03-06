using LaptopMart.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace LaptopMart.ApplicationDb
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Product> ProductTable { get; set; }

        public DbSet<Supplier> SupplierTable { get; set; }

        public DbSet<Cart> CartTable { get; set; }

        public DbSet<CartItem> CartItemTable { get; set; }

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
            modelBuilder.Entity<Cart>().ToTable("CartTable").HasMany(c => c.CartItems).WithRequired().WillCascadeOnDelete(true);
            modelBuilder.Entity<CartItem>().ToTable("CartItemTable");


            modelBuilder.Entity<Product>()
               .HasMany(p => p.Categories)
               .WithMany(c => c.Products)
               .Map(cs =>
               {
                   cs.MapLeftKey("ProductId");
                   cs.MapRightKey("CategoryId");
                   cs.ToTable("ProductCategoryTable");
               });

            modelBuilder.Entity<Product>().HasRequired<Supplier>(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey<string>(p => p.SupplierName);

            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Product>().Property(p => p.Image).IsRequired();
            modelBuilder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Category>().Property(p => p.Description).HasMaxLength(300);
            modelBuilder.Entity<Category>().Property(p => p.ParentId).IsRequired();
            modelBuilder.Entity<Supplier>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Supplier>().Property(p => p.Description).IsRequired().HasMaxLength(300);
            modelBuilder.Entity<Supplier>().HasKey(k => k.Name);
            modelBuilder.Entity<Supplier>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ApplicationUser>().Property(p => p.Name).IsRequired().HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}