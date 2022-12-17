using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Infrastructure.Exceptions;

[ExcludeFromCodeCoverage]
public class UnprocessableEntityException : CanopusException
{
    public UnprocessableEntityException(string message) : base(StatusCodes.Status422UnprocessableEntity, message)
    {
        
    }
}