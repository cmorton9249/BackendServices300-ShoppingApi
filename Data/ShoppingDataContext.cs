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
    }
}
