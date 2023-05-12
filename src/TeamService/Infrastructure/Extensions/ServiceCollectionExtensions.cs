using System.Reflection;
using TeamService.Services.TeamsService;
using Microsoft.OpenApi.Models;
using MassTransit;

namespace TeamService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddTransient<ITeamsService, TeamsService>();

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

    public static IServiceCollection AddSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Project Management System - Team Service",
                        Version = "v1"
                    });

                c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
}
