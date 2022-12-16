using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Canopus.API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllers();

services.AddDbContext<CanopusContext>(opts =>
{
    var connectionString = configuration.GetConnectionString("CanopusDb");

    if (string.IsNullOrEmpty(connectionString))
    {
        opts.UseInMemoryDatabase(Guid.NewGuid().ToString());

        return;
    }

    opts.UseSqlServer(connectionString, sqlOpts =>
    {
        sqlOpts.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    });
});

services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Canopus Challenge - ília"
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    
    opts.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

// app.UseSwagger();

app.UseSwaggerUI(opts =>
{
    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Canopus.API");
    opts.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program {}