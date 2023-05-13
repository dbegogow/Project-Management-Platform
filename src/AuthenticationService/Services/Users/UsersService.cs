using AuthenticationService.Infrastructure.Extensions;
using AuthenticationService.Models.Data;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

        this._usersCollection = mongoDb
            .GetCollection<User>(UsersCollectionName);

        this._rolesCollection = mongoDb
            .GetCollection<Role>(RolesCollectionName);
    }

    public async Task<bool> ValidateUserEmailExist(string email)
    {
        var exist = await this._usersCollection
            .Find(u => u.Email == email)
            .AnyAsync();

        if (exist)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> ValidateUser(string id, IEnumerable<string> roleNames)
    {
        var userRoleId = await this._usersCollection
            .Find(u => u.Id == id)
            .Project(u => u.Roles.First())
            .FirstOrDefaultAsync();

        if (userRoleId == null)
        {
            return await Task.FromResult(false);
        }

        var userRoleName = await this._rolesCollection
            .Find(r => r.Id == userRoleId)
            .Project(r => r.Name)
            .FirstOrDefaultAsync();

        if (!roleNames.Contains(userRoleName))
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }
}
