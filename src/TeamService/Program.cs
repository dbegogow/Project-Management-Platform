using TeamService.Infrastructure.Extensions;
using Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddEndpointsApiExplorer()
    .AddApplicationServices()
    .AddMassTransiteWithRabbitMq(configuration)
    .AddSwagger()
    .AddControllers();

var app = builder.Build();

app
    .UseSwaggerUI()
    .UseHttpsRedirection();

app.MapControllers();

app.Run();
