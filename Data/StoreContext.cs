using Microsoft.EntityFrameworkCore;

namespace API_ECOMMERCE
{
     public class StoreContext: DbContext
     {
        public StoreContext(DbContextOptions options): 
        base(options)
        {

        }

        public DbSet<Product> Products { get; set; } //Products sera el nombre que se le dara en BBD
        

     }
}