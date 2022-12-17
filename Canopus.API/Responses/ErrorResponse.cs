using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Responses;

[ExcludeFromCodeCoverage]
public class ErrorResponse
{
    public ErrorResponse(string message, int code)
    {
        Message = message;
        Code = code;
    }

    public string Message { get; }

    public int Code { get; }
}