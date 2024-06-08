using Microsoft.Extensions.DependencyInjection;

namespace JuntoSeguros.Domain;

public static class Setup
{
    public static IServiceCollection AddDomain(this IServiceCollection service)
    {
        return service;
    }
}