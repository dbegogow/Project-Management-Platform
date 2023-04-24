namespace AuthenticationService.Services;

public interface IAuthenticationService
{
    string GenerateJwtToken(string userId, string userName, string secret);
}
