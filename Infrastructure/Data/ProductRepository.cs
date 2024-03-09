using Core;
using Core.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        public ProductRepository(StoreContext context){
            this.context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {          
            return await context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p=> p.Id == id);
        }   

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            //.ToListAsync() es el punto donde se envia la solicitud o consulta a la bbd
            //Es el punto donde se hace la solicitud y se nos devuelven los datos
            return await context.Products
            .Include(p => p.ProductType)
            .Include(p=> p.ProductBrand)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await context.ProductTypes.ToArrayAsync();
        }
    }
}