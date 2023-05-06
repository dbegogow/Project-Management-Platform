using TeamService.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc;

namespace TeamService.Infrastructure.Attributes;

public class AuthorizeUserAttribute : Attribute, IAsyncActionFilter
{
    private const string RoleQueryParam = "Role";
    private const string AuthorizationHeader = "Authorization";

    private readonly string[] _roles;
    private readonly HttpClient _httpClient;

    public AuthorizeUserAttribute(params string[] roles)
    {
        this._roles = roles;
        this._httpClient = new HttpClient();
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        httpContext.Request.Headers
            .TryGetValue(AuthorizationHeader, out var authHeaderValues);

        var authHeaderValue = authHeaderValues.FirstOrDefault();

        if (authHeaderValue == null)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            return;
        }

        var configuration = context.HttpContext.RequestServices
            .GetService(typeof(IConfiguration)) as IConfiguration;

        var authServiceConfig = configuration.GetAuthenticationServiceConfigurations();
        var validateTokenUri = authServiceConfig.ValidateTokenUri;

        var uri = QueryHelpers
            .AddQueryString(
                validateTokenUri,
                new Dictionary<string, string>
                {
                    [RoleQueryParam] = string.Join(",", this._roles)
                });

        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add(AuthorizationHeader, authHeaderValue);

        var response = await this._httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            context.Result = new StatusCodeResult((int)response.StatusCode);
            return;
        }

        await next();
    }
}
