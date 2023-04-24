using System.Text;
using AuthenticationService.Models.Data;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            .AddMongoDbStores<User, Role, string>(
                mongoDbConfiguration.ConnectionString,
                mongoDbConfiguration.Name);

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection service,
            IConfiguration configuration)
    {
        var jwtConfiguration = configuration.GetJwtConfigurations();

        var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret);

        service
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return service;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddTransient<IIdentityService, IdentityService>();
}
