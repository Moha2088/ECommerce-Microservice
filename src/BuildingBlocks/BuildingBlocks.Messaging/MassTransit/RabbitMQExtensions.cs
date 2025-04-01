using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class RabbitMQExtensions
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services,
         IConfiguration con, Assembly? assembly = null)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
            {
                cfg.AddConsumers(assembly);
            }

            cfg.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(con["MessageBroker:Host"]!), host =>
                {
                    host.Username(con["MessageBroker:UserName"]!);
                    host.Password(con["MessageBroker:Password"]!);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}