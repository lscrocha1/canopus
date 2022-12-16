using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Canopus.API.Infrastructure.Context;

[ExcludeFromCodeCoverage]
public class CanopusContext : DbContext
{
    public CanopusContext(DbContextOptions<CanopusContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}