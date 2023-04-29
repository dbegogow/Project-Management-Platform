using AuthenticationService.Models.Data;
using System.Security.Claims;

namespace AuthenticationService.Infrastructure.Extensions;

public static class IdentityExtensions
{
    public static string GetId(this ClaimsPrincipal user)
        => GetClaimValue(user, ClaimTypes.NameIdentifier);

    public static string GetRole(this ClaimsPrincipal user)
        => GetClaimValue(user, ClaimTypes.Role);

    private static string GetClaimValue(ClaimsPrincipal user, string claimType)
        => user
            .Claims
            .FirstOrDefault(c => c.Type == claimType)
            ?.Value;
}
