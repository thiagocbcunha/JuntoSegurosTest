using JuntoSeguros.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace JuntoSeguros.Onboarding.Command.Api.Middware;

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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An business exception occurred.");

        List<string> errors = new List<string>();
        HttpStatusCode statusCode;

        if (exception is ValidationException appException)
        {
            errors.AddRange(appException.Errors);
            statusCode = HttpStatusCode.BadRequest;
        }
        else
        {
            errors.Add(exception.Message);
            statusCode = HttpStatusCode.InternalServerError;
            if (exception is BusinessException)
                statusCode = HttpStatusCode.BadRequest;
        }

        var response = new
        {
            errors = errors,
            status = statusCode
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}