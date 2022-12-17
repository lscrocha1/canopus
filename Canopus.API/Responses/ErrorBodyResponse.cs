using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Responses;

[ExcludeFromCodeCoverage]
public class ErrorBodyResponse
{
    public ErrorBodyResponse(string message, int code)
    {
        Message = message;
        Code = code;
    }

    public string Message { get; }

    public int Code { get; }
}