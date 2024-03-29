using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Core;
using System.Reflection;
using Infrastructure.Data.Config;
namespace Infrastructure
{
     public class StoreContext: DbContext
     {
        public StoreContext(DbContextOptions options): 
        base(options)
        {

         
        }

        public DbSet<Product> Products { get; set; } //Products sera el nombre que se le dara en BBD
        
        public DbSet<ProductBrand> ProductBrands { get; set; }
        
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
         
         base.OnModelCreating(modelBuilder);

         modelBuilder.ApplyConfiguration(new ProductConfig());
        }
     }
}