using Canopus.API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opts =>
{
    opts.RoutePrefix = string.Empty;
});

app.MapGet("/", () => "Hello World!");

app.Run();