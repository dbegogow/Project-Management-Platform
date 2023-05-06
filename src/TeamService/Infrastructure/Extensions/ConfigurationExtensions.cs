using TeamService.Infrastructure.Configurations;

namespace TeamService.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static MongoDbConfig GetMongoDbConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

    public static AuthenticationServiceConfig GetAuthenticationServiceConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(AuthenticationServiceConfig)).Get<AuthenticationServiceConfig>();
}
