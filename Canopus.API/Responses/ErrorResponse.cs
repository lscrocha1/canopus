using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Responses;

[ExcludeFromCodeCoverage]
public class ErrorResponse
{
    public ErrorResponse(string message, int code)
    {
        Error = new ErrorBodyResponse(message, code);
    }
    
    public ErrorBodyResponse Error { get; }
}