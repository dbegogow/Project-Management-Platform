using AuthenticationService.Models.Messages;
using MassTransit;

namespace AuthenticationService.Services.Publishing;

public class PublishingService : IPublishingService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PublishingService(IPublishEndpoint publishEndpoint)
        => this._publishEndpoint = publishEndpoint;

    public async Task PublishCreatedUser(string id, string username)
        => await this._publishEndpoint
            .Publish(new UserCreatedMessageModel
            {
                Id = id,
                Username = username
            });
}
