using JuntoSeguros.Domain;
using JuntoSeguros.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace JuntoSeguros.Infra.IoC;

public static class Setup
{
    public static IServiceCollection SetupApplication(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDomain();
        service.AddApplication();
        service.AddInfra(configuration);

        return service;
    }
}