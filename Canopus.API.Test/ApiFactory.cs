using System.Diagnostics.CodeAnalysis;
using Canopus.API.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Canopus.API.Test;

[ExcludeFromCodeCoverage]
public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((services) =>
        {
            services.AddDbContext<CanopusContext>(opts =>
            {
                opts.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });
        });
        
        base.ConfigureWebHost(builder);
    }
}