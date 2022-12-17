using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Infrastructure.Exceptions;

[ExcludeFromCodeCoverage]
public class NotFoundException : CanopusException
{
    public NotFoundException(string message, int code = StatusCodes.Status404NotFound) : base(code, message)
    {
        
    }
}