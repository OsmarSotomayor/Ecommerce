using API.Errors;
using API.Middleware;
using Core.interfaces;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.
    GetConnectionString("DefaultConnection"));
});

//Implementamos el servicio para usar la interface
//AddScope es lo que indica cuando "vivira el servicio"
builder.Services.
AddScoped<IProductRepository, ProductRepository>();

builder.Services.
AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Para automapper
//Registrara los mapping profiles cuando inicie la aplicacion
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Improving the validation error response
builder.Services.Configure<ApiBehaviorOptions>(options =>
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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

//para redirigirlos a nuestro controlador de errores 
app.UseStatusCodePagesWithReExecute("/errors/{0}");


app.UseSwagger();
app.UseSwaggerUI();


//para agregar contenido estatico como imagenes
app.UseStaticFiles();

//app.UseHttpsRedirection(); causara warnings no necesarios para el proyecto
app.UseAuthorization();

app.MapControllers(); //Esta linea nos permite ligar los endpoints con los controladores

//nos ayuda a inyectar nuevos servicios 
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
