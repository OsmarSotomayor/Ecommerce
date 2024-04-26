using API.Errors;
using API.Extensions;
using API.Middleware;
using Core.interfaces;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Add services to the container.
builder.Services.addApplicationServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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
