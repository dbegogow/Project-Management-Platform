using AuthenticationService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddEndpointsApiExplorer()
    .AddIdentity(configuration)
    .AddJwtAuthentication(configuration)
    .AddApplicationServices()
    .AddSwagger()
    .AddControllers();

var app = builder.Build();

app
    .UseSwaggerUI()
    .UseHttpsRedirection()
    .UseAuthorization()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();
