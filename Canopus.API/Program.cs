using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Canopus.API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Canopus.API.Infrastructure.Exceptions;
using Canopus.API.Services.Application.Customer;
using Canopus.API.Services.Application.Order;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllers();

services.AddDbContext<CanopusContext>(opts =>
{
    var connectionString = configuration.GetConnectionString("CanopusDb");

    opts.UseSqlServer(connectionString, sqlOpts =>
    {
        sqlOpts.EnableRetryOnFailure(
            5,
            TimeSpan.FromSeconds(30),
            null);
    });
});

services.AddScoped<IOrderService, OrderService>();
services.AddScoped<Canopus.API.Services.Domain.Order.IOrderService, Canopus.API.Services.Domain.Order.OrderService>();

services.AddScoped<ICustomerService, CustomerService>();
services
    .AddScoped<Canopus.API.Services.Domain.Customer.ICustomerService,
        Canopus.API.Services.Domain.Customer.CustomerService>();

services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Canopus - A code challenge"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    opts.IncludeXmlComments(xmlPath, true);
});

var app = builder.Build();

RunMigrations(app.Services).Wait();

app.UseSwagger();

app.UseSwaggerUI(opts =>
{
    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Canopus.API");
    opts.RoutePrefix = string.Empty;
});

app.MapControllers();

app.AddExceptionHandler();

app.Run();

async Task RunMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<CanopusContext>();

    await context.Database.EnsureCreatedAsync();
}

[ExcludeFromCodeCoverage]
public partial class Program
{
}