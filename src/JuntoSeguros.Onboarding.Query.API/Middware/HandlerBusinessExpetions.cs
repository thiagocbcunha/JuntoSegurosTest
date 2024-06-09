using JuntoSeguros.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace JuntoSeguros.Onboarding.Query.Api.Middware;

public class HandlerBusinessExpetions
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HandlerBusinessExpetions> _logger;

    public HandlerBusinessExpetions(RequestDelegate next, ILogger<HandlerBusinessExpetions> logger)
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
        catch (BusinessException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, BusinessException exception)
    {
        _logger.LogError(exception, "An business exception occurred.");

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            Message = exception.Message,
            StatusCode = context.Response.StatusCode,
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
