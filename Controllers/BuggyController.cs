using API.Errors;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController: BaseApiController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }

        //Llenaremos estos metodos con algun tipo de error 
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            //simularemos la busqueda de un producto que no existe
            var thing = context.Products.Find(43);

            if(thing == null)
            { 
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            //Aqui simularemos cuando apliquemos un metodo en algo nulo
            var thing = context.Products.Find(43);

            //esto generara una excepcion pues thing es null
            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
