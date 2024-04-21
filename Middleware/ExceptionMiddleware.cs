using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        //inyectamos RequestDelegate y ILogger
        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger, IHostEnvironment env) {
            Next = next;
            Logger = logger;
            Environment = env;
        }
        public RequestDelegate Next { get; }
        public ILogger<ExceptionMiddleware> Logger { get; }
        public IHostEnvironment Environment { get; }

        //Agregamos el metodo que sera nuestro try catch mmidleware
        //sera nuestro exception handling 
        public async Task InvokeAsync(HttpContext context)
        {
            //aqui usaremos RequestDelegate para procesa el HTTP request
            try
            {
                //if there is no exception then the request moves on it next stage
                await Next(context);  
            }
            catch(Exception ex) 
            {
                Logger.LogError(ex, ex.Message);
                //Agregaremos tambien el response to the client
                context.Response.ContentType= "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //creamos una repuesta para produccion y otra para desarrollo
                //daremos mas respuestas si estamos en desarrollo
                var response = Environment.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message,
                    ex.StackTrace.ToString()) 
                    :new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var jsonResponse = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
