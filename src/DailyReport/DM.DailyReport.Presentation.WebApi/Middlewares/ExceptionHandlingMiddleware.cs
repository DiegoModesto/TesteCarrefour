using System.Text.Json;
using DM.Application.Exceptions;
using DM.SharedKernel.CustomErrors;

namespace DM.Presentation.WebApi.Middlewares;

internal sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext ctx, Exception ex)
    {
        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode = ex switch
        {
            BadRequestException or ValidatioNException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var errors = Array.Empty<ApiError>();

        if (ex is ValidatioNException validatioNException)
        {
            errors = validatioNException.Errors
                .SelectMany(
                    kvp => kvp.Value,
                    (kvp, value) => new ApiError(kvp.Key, value))
                .ToArray();
        }

        var response = new
        {
            status = ctx.Response.StatusCode,
            message = ex.Message,
            errors
        };

        await ctx.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private record ApiError(string PropertyName, string ErrorMessage);
}