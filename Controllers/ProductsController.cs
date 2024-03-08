using Core;
using Core.interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //Nuestro controlador no necesitara alguna vista como soporte pues eso
    //sera proporcionado por Angular 
    [ApiController] //agregarmos el atributo ApiController porque lo general mejora el código para escribir menos
    [Route("api/[controller]")] //https://localhost:5001/Products
    public class ProductsController: ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductsController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> getProducts()
        {
            var products = await repository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProduct(int id)
        {
            return await repository.GetProductByIdAsync(id);
        }

        [HttpGet("brands")] //Sin el ok este metodo daria error
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductBrands(){
           return Ok(await repository.GetProductBrandsAsync());
        } 
        //Aunque es una lista tipo generica es necesario el Ok

        [HttpGet("types")] 
        public async Task<ActionResult<IReadOnlyList<ProductType>>> getProductTypes(){
           return Ok(await repository.GetProductTypesAsync());
        }
        

        
    }
 }

