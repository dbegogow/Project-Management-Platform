using TeamService.Infrastructure.Attributes;
using TeamService.Infrastructure.Configurations;

namespace TeamService.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static MongoDbConfig GetMongoDbConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

    public static AuthorizeUserAttribute GetAuthenticationServiceConfigurations(this IConfiguration configuration)
            => configuration.GetSection(nameof(AuthorizeUserAttribute)).Get<AuthorizeUserAttribute>();
}
