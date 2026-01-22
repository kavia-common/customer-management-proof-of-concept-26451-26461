using CustomerManagement.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Middleware;

/// <summary>
/// Converts application-level exceptions (e.g., FluentValidation via pipeline) into RFC7807 ProblemDetails.
/// </summary>
public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (RequestValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";

            // ValidationProblemDetails expects IDictionary<string, string[]> (not IReadOnlyDictionary).
            var problem = new ValidationProblemDetails(ex.Errors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value))
            {
                Title = "Validation failed",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = context.Request.Path
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
