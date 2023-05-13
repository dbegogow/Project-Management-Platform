using Common.Configurations;
using Microsoft.Extensions.Configuration;

namespace Common.Extensions;

public static class ConfigurationExtensions
{
    public static RabbitMqConfig GetRabbitMqConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>();

    public static ServiceConfig GetServiceConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(ServiceConfig)).Get<ServiceConfig>();
}
