using System.Diagnostics.CodeAnalysis;
using Canopus.API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Canopus.API.Test;

[ExcludeFromCodeCoverage]
public static class Helpers
{
    public static CancellationToken GetCancellationToken()
    {
        var source = new CancellationTokenSource();

        return source.Token;
    }

    public static CanopusContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<CanopusContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        return new CanopusContext(options);
    }
}