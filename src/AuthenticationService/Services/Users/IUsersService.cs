namespace AuthenticationService.Services.Users;

public interface IUsersService
{
    Task<bool> ValidateUserEmailExist(string email);

    Task<bool> ValidateUser(string id, IEnumerable<string> role);
}
