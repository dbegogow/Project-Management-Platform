using Microsoft.AspNetCore.Mvc.Filters;
using TeamService.Infrastructure.Extensions;

namespace TeamService.Infrastructure.Attributes;

public class AuthorizeUserAttribute : Attribute, IAsyncActionFilter
{
    private readonly string[] _userRoles;

    public AuthorizeUserAttribute(string[] userRoles)
        => this._userRoles = userRoles;

    public Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var configuration = context.HttpContext.RequestServices
            .GetService(typeof(IConfiguration)) as IConfiguration;

        var apiKey = configuration.GetMongoDbConfigurations();
    }
}
