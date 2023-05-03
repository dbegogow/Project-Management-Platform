using AuthenticationService.Infrastructure.Extensions;
using AuthenticationService.Models.Data;
using MongoDB.Driver;

namespace AuthenticationService.Services.Users;

public class UsersService : IUsersService
{
    private const string UsersCollectionName = "Users";
    private const string RolesCollectionName = "Roles";

    private readonly IMongoCollection<User> _usersCollection;
    private readonly IMongoCollection<Role> _rolesCollection;

    public UsersService(IConfiguration configuration)
    {
        var mongoDbConfiguration = configuration.GetMongoDbConfigurations();

        var mongoClient = new MongoClient(mongoDbConfiguration.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(mongoDbConfiguration.Name);

        this._usersCollection = mongoDb.GetCollection<User>(UsersCollectionName);
        this._rolesCollection = mongoDb.GetCollection<Role>(RolesCollectionName);
    }

    public async Task<bool> ValidateUser(string id, string roleName)
    {
        var usersQuerableCollection = this._usersCollection.AsQueryable();
        var rolesQuerableCollection = this._rolesCollection.AsQueryable();

        var userRoles = from u in usersQuerableCollection
                        

        var a = userRoles.

        return true;
    }
}
