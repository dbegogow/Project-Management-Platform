using AuthenticationService.Infrastructure.Configurations;

namespace AuthenticationService.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static MongoDbConfig GetMongoDbConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

    public static JwtConfig GetJwtConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();

    public static RabbitMqConfig GetRabbitMqConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>();

    public static ServiceConfig GetServiceConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(ServiceConfig)).Get<ServiceConfig>();
}