using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Test;

[ExcludeFromCodeCoverage]
public static class Helpers
{
    public static CancellationToken GetCancellationToken()
    {
        var source = new CancellationTokenSource();

        return source.Token;
    }
}