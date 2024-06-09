using JuntoSeguros.Enterprise.Library.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace JuntoSeguros.Enterprise.Library.Security;

public static class EnterpriseSecurityConfiguration
{
    public static IServiceCollection AddEnterpriseSecurity(this IServiceCollection services)
    {
        services.AddScoped<IEnterpriseSecurity, EnterpriseSecurity>();

        return services;
    }
}
