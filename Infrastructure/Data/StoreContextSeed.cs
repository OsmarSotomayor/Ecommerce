
using System.Text.Json;
using Core;

namespace Infrastructure
{
     public class StoreContextSeed
     {
        public static async Task SeedAsync(StoreContext context){
            if(!context.ProductBrands.Any()){

                var brandsData = File.ReadAllText("C:/Users/PC ONE/Ecommerce/Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands); 
                //Este metodo AddRange no sera asincrono porque no llamamos a la bbd como tal
                //Es un seguimiento de memoria que hace EFC
            }

            if(!context.ProductTypes.Any()){

                var typesData = File.ReadAllText("C:/Users/PC ONE/Ecommerce/Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types); 
            }

            if(!context.Products.Any()){

                var productsData = File.ReadAllText("C:/Users/PC ONE/Ecommerce/Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products); 
            }

            //para que guarde los datos en la bbd
                if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }    
     }
}