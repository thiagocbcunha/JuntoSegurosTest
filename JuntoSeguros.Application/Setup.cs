using JuntoSeguros.Application.Consumers;
using Microsoft.Extensions.DependencyInjection;
using JuntoSeguros.Application.Command.PersonCommands;

namespace JuntoSeguros.Application;

public static class Setup
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<PersonConsumer>();
        service.AddScoped<PersonAccessConsumer>();
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePersonCommand>());

        return service;
    }
}
