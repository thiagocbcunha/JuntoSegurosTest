using Microsoft.AspNetCore.Builder;

namespace JuntoSeguros.Enterprise.Library.Logging.Extensions;

public static class EnterpriseLogMiddware
{
    public static IApplicationBuilder AddEnterpriseLogMiddware(this IApplicationBuilder app)
    {
        app.UseMiddleware<UnhandledExceptionMiddware>();
        return app;
    }
}