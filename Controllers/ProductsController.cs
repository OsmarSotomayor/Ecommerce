using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //Nuestro controlador no necesitara alguna vista como soporte pues eso
    //sera proporcionado por Angular 
    [ApiController] //agregarmos el atributo ApiController porque lo general mejora el código para escribir menos
    [Route("api/[controller]")] //https://localhost:5001/Products
    public class ProductsController: ControllerBase
    {
        [HttpGet]
        public string getProducts()
        {
            return "this will bi a list of products";
        }
        [HttpGet("{id}")]
        public string getProduct(int id)
        {
            return "this will bi a list of products";
        }
    }
 }

