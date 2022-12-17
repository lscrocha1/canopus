using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Infrastructure.Exceptions;

[ExcludeFromCodeCoverage]
public class NotFoundException : CanopusException
{
    public NotFoundException(string message) : base(StatusCodes.Status404NotFound, message)
    {
        
    }
}