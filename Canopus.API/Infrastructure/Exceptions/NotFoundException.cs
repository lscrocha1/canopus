using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Infrastructure.Exceptions;

[ExcludeFromCodeCoverage]
public class NotFoundException : CanopusException
{
    public NotFoundException(int code, string message) : base(code, message)
    {
        
    }
}