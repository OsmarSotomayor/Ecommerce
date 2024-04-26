using API.Errors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("errors/{code}")] //la ruta del controlador se va a sobrescribir
    [ApiExplorerSettings(IgnoreApi =true)] //con esto ignora el error controller
    public class ErrorsController: BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code)); //usamos nuestra clase ApiResponse
        }
        //como tomamos el request del cliente y lo pasamos a este controller ?
        //usamos middleware
    }
}
