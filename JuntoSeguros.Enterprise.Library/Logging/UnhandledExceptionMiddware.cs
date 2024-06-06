using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JuntoSeguros.Enterprise.Library.Logging;


public class UnhandledExceptionMiddware : IMiddleware
{
    private readonly ILogger<UnhandledExceptionMiddware> _logger;

    public UnhandledExceptionMiddware(ILogger<UnhandledExceptionMiddware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            const string message = "An unhandled exception has occurred while executing the request.";
            _logger.LogError(exception, message);

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}