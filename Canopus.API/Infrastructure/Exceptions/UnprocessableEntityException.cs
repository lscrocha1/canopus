using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Infrastructure.Exceptions;

[ExcludeFromCodeCoverage]
public class UnprocessableEntityException : CanopusException
{
    public UnprocessableEntityException(string message, int code = StatusCodes.Status422UnprocessableEntity) : base(code, message)
    {
        
    }
}