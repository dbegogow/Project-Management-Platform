namespace AuthenticationService.Services.Identity;

public interface IIdentityService
{
    string GenerateJwtToken(string userId, string userName, string role);
}
