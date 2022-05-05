using Microsoft.EntityFrameworkCore;
using Shop.Entities.Concrete;

namespace Shop.Dal.Concrete.EntityFramework.ContextBase
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .UseLazyLoadingProxies(false)
        .UseSqlServer("Connecting String");

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    }
}