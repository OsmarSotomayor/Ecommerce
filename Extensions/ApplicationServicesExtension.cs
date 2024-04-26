using Core.interfaces;
using Infrastructure.Data;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtension
    {
        //el primer parametro es la cosa que extendemos
        public static IServiceCollection addApplicationServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(configuration.
                GetConnectionString("DefaultConnection"));
            });

            //Implementamos el servicio para usar la interface
            //AddScope es lo que indica cuando "vivira el servicio"
            services.
            AddScoped<IProductRepository, ProductRepository>();

            services.
            AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Para automapper
            //Registrara los mapping profiles cuando inicie la aplicacion
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Improving the validation error response
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });


            return services;
        }
    }
}
