using JuntoSeguros.Application.Consumers;
using Microsoft.Extensions.DependencyInjection;
using JuntoSeguros.Application.Command.PersonCommands;
using JuntoSeguros.Application.Validation;
using MediatR;

namespace JuntoSeguros.Application;

public static class Setup
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<PersonConsumer>();
        service.AddScoped<PersonAccessConsumer>();
        service.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePersonCommand>());

        return service;
    }
}
