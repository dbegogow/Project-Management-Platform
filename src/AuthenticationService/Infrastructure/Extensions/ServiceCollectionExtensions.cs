using AuthenticationService.Models.Data;

namespace AuthenticationService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var mongoDbConfiguration = configuration.GetMongoDbConfigurations();

        services
            .AddIdentity<User, Role>()
            .AddMongoDbStores<User, Role, Guid>(
                mongoDbConfiguration.ConnectionString,
                mongoDbConfiguration.Name);

        return services;
    }
}
