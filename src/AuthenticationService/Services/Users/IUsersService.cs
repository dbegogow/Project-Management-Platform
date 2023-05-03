namespace AuthenticationService.Services.Users;

public interface IUsersService
{
    Task<bool> ValidateUser(string id, string role);
}
