using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ShoppingApi.Data
{
    public class ShoppingDataContext: DbContext
    {
        public ShoppingDataContext(DbContextOptions<ShoppingDataContext> options): base(options)
        {

        }

        public IQueryable<Product> GetItemsInInventory()
        {
            return Products.Where(p => p.InInventory);
        }

        public IQueryable<Product> GetItemsFromCategory(string category)
        {
            return GetItemsInInventory().Where(p => p.Category == category);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CurbsideOrder> CurbsideOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Category).HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Description).HasMaxLength(200);
            modelBuilder.Entity<Product>().Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
        }
    }
}
