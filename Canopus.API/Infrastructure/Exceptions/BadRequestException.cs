using System.Diagnostics.CodeAnalysis;
using Canopus.API.Infrastructure.Constants;

namespace Canopus.API.Infrastructure.Exceptions;

[ExcludeFromCodeCoverage]
public class BadRequestException : CanopusException
{
    public BadRequestException(string message) : base(StatusCodes.Status400BadRequest, message)
    {
    }
}