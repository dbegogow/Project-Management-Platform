using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.Infrastructure.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}
