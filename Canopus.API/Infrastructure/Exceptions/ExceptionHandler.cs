using System.Diagnostics.CodeAnalysis;
using Canopus.API.Infrastructure.Constants;
using Canopus.API.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Canopus.API.Infrastructure.Exceptions;

[ExcludeFromCodeCoverage]
public static class ExceptionHandler
{
    public static void AddExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exHandler =>
        {
            exHandler.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                var message = ExceptionConstant.UnhandledException;

                var code = StatusCodes.Status500InternalServerError;
                
                if (exceptionHandlerPathFeature?.Error is CanopusException canopusException)
                {
                    message = canopusException.Message;
                    code = canopusException.Code;
                }

                await context.Response.WriteAsJsonAsync(new ObjectResult(new ErrorResponse(message, code))
                {
                    StatusCode = code
                });
            });
        });
    }
}