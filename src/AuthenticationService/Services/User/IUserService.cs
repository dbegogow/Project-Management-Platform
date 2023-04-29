namespace AuthenticationService.Services.User;

public interface IUserService
{
    bool ValidateUser(string id, string[] roleIds);
}
