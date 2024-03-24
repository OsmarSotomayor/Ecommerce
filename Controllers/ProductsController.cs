using Core;
using Core.interfaces;
using Core.Specifications;
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
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;

        public ProductsController(IGenericRepository<Product> productRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo)
        {
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> getProducts()
        {
            var specification = new ProductsWithTypesAndBrandsSpecification();
            //Con sppecification tenemos ya un nombre significativo que nos dic que buscamos

            var products = await productRepo.listAsync(specification);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProduct(int id)
        {
            //En esta specification llamamos al constructor con parametro id
            var specification = new ProductsWithTypesAndBrandsSpecification(id);
 
            return await productRepo.GetEntityWithSpec(specification);
        }

        [HttpGet("brands")] //Sin el ok este metodo daria error
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductBrands(){

           return Ok(await productBrandRepo.ListAllAsync());
        } 
        //Aunque es una lista tipo generica es necesario el Ok
        
        [HttpGet("types")] 
        public async Task<ActionResult<IReadOnlyList<ProductType>>> getProductTypes(){
           return Ok(await productTypeRepo.ListAllAsync());
        }         
    }
 }

