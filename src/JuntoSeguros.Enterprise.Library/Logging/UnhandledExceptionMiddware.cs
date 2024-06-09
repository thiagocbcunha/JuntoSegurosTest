using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JuntoSeguros.Enterprise.Library.Logging;

public class UnhandledExceptionMiddware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UnhandledExceptionMiddware> _logger;

    public UnhandledExceptionMiddware(RequestDelegate next, ILogger<UnhandledExceptionMiddware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An error occurred while processing your request."
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}