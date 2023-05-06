using TeamService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddControllers();

var app = builder.Build();

app
    .UseSwaggerUI()
    .UseHttpsRedirection();

app.MapControllers();

app.Run();
