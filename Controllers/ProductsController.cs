using API.Dtos;
using AutoMapper;
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
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductsToReturnDTO>>> getProducts()
        {
            var specification = new ProductsWithTypesAndBrandsSpecification();
        
            var products = await productRepo.listAsync(specification);
            
            return Ok(mapper.Map<IReadOnlyList<Product>, 
            IReadOnlyList<ProductsToReturnDTO>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsToReturnDTO>> getProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);
            var product =  await productRepo.GetEntityWithSpec(specification);
            
            //devolvemos el producto con automaper
            return mapper.Map<Product, ProductsToReturnDTO>(product);
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

