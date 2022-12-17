using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Infrastructure.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class CanopusException : Exception
{
    protected CanopusException(int code, string message) : base(message)
    {
        Code = code;
    }

    public int Code { get; } = StatusCodes.Status500InternalServerError;
}