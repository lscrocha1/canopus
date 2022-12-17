using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Infrastructure.Exceptions;

[ExcludeFromCodeCoverage]
public class UnprocessableEntityException : CanopusException
{
    public UnprocessableEntityException(int code, string message) : base(code, message)
    {
        
    }
}