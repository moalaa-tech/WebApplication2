using AutoMapper;
using CRM.WebApi.Controllers;
using CRM.WebApi.DbContext;
using CRM.WebApi.DbContext.EasyOrderModels;
using CRM.WebApi.EasyOrder;
using CRM.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("defaultConnection"));
});



builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<EasyOrderDto, EasyOrderRequest>().ReverseMap();
    cfg.CreateMap<EasyOrderProductDto, EasyOrderProduct>().ReverseMap();
    cfg.CreateMap<CartItemDto, EasyOrderCartItem>().ReverseMap();
    cfg.CreateMap<VariantDto, EasyOrderVariant>().ReverseMap();
    cfg.CreateMap<VariationPropDto, EasyOrderVariationProp>().ReverseMap();



});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseMiddleware<GlobalExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationContext>();
        app.UseMiddleware<RequestLoggingMiddleware>(context);
        //app.UseMiddleware<GlobalExceptionMiddleware>(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}




app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
