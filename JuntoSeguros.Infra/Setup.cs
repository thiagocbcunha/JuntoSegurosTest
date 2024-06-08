using MassTransit;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Infra.Mongo;
using JuntoSeguros.Infra.RabbitMQ;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Application.Consumers;
using Microsoft.Extensions.Configuration;
using JuntoSeguros.Infra.Dapper.Contracts;
using JuntoSeguros.Infra.RabbitMQ.Options;
using JuntoSeguros.Infra.Dapper.Connection;
using JuntoSeguros.Infra.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JuntoSeguros.Infra;

public static class Setup
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRabbitMQ(configuration);
        services.AddScoped<IMessagingSender, MessageSender>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IConnectionFactory, DapperConnectionFactory>();
        services.AddScoped<IPersonAccessRepository, PersonAccessRepository>();

        services.AddScoped<IPersonNSqlRepository, MongoPersonRepository>();
        services.AddScoped<IPersonAccessNSqlRepository, MongoPersonAccessRepository>();

        return services;
    }

    private static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitConfiguration = new RabbitConfiguration();
        configuration.GetRequiredSection(nameof(RabbitConfiguration)).Bind(rabbitConfiguration);


        services.AddMassTransit(m =>
        {
            m.AddConsumer<PersonConsumer>();
            m.AddConsumer<PersonAccessConsumer>();

            m.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(rabbitConfiguration.Endpoint, "/", c =>
                {
                    c.Email(rabbitConfiguration.Email);
                    c.Password(rabbitConfiguration.Password);
                });

                cfg.ConfigureEndpoints(ctx);

                Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Message<PersonDto>(x => x.SetEntityName("JuntoSeguros.PersonDto.Event"));
                    cfg.Message<PersonDto>(x => x.SetEntityName("JuntoSeguros.PersonAccessDto.Event"));
                });
            });
        });

        return services;
    }
}