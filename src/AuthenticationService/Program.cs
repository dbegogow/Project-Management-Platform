using AuthenticationService.Infrastructure.Extensions;
using Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddEndpointsApiExplorer()
    .AddIdentity(configuration)
    .AddJwtAuthentication(configuration)
    .AddApplicationServices()
    .AddMassTransiteWithRabbitMq(configuration)
    .AddSwagger()
    .AddControllers();

var app = builder.Build();

app
    .UseSwaggerUI()
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();
