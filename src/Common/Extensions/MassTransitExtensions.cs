using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace Common.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransiteWithRabbitMq(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddMassTransit(c =>
        {
            c.AddConsumers(Assembly.GetEntryAssembly());

            c.UsingRabbitMq((context, configurator) =>
            {
                var rabbitMqConfiguration = configuration.GetRabbitMqConfigurations();

                configurator.Host(rabbitMqConfiguration.Host);

                var serviceConfiguration = configuration.GetServiceConfigurations();

                configurator.ConfigureEndpoints(
                    context,
                    new KebabCaseEndpointNameFormatter(serviceConfiguration.Name, false));

                configurator.UseMessageRetry(
                    retryConfigurator => retryConfigurator.Interval(3, TimeSpan.FromSeconds(5)));
            });
        });
}
