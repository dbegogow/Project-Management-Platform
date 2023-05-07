namespace AuthenticationService.Services.Publishing;

public interface IPublishingService
{
    Task PublishCreatedUser(string id, string username);
}
