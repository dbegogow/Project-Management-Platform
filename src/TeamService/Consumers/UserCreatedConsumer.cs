using TeamService.Models.Messages;
using TeamService.Models.Data;
using TeamService.Infrastructure.Extensions;
using MongoDB.Driver;
using MassTransit;

namespace TeamService.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreatedMessageModel>
{
    private const string UsersCollectionName = "Users";

    private readonly IMongoCollection<User> _usersCollection;

    public UserCreatedConsumer(IConfiguration configuration)
    {
        var mongoDbConfiguration = configuration.GetMongoDbConfigurations();

        var mongoClient = new MongoClient(mongoDbConfiguration.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(mongoDbConfiguration.Name);

        this._usersCollection = mongoDb
            .GetCollection<User>(UsersCollectionName);
    }

    public async Task Consume(ConsumeContext<UserCreatedMessageModel> context)
    {
        var message = context.Message;

        var user = await this._usersCollection
            .Find(u => u.Id == message.Id)
            .FirstOrDefaultAsync();

        if (user != null)
        {
            return;
        }

        user = new User
        {
            Id = message.Id,
            Username = message.Username
        };

        await this._usersCollection.InsertOneAsync(user);
    }
}
