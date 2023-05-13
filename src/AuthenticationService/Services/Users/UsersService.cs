using AuthenticationService.Infrastructure.Extensions;
using AuthenticationService.Models.Data;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AuthenticationService.Services.Users;

public class UsersService : IUsersService
{
    private const string UsersCollectionName = "Users";
    private const string RolesCollectionName = "Roles";

    private readonly IMongoQueryable<User> _usersCollection;
    private readonly IMongoQueryable<Role> _rolesCollection;

    public UsersService(IConfiguration configuration)
    {
        var mongoDbConfiguration = configuration.GetMongoDbConfigurations();

        var mongoClient = new MongoClient(mongoDbConfiguration.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(mongoDbConfiguration.Name);

        this._usersCollection = mongoDb
            .GetCollection<User>(UsersCollectionName)
            .AsQueryable();

        this._rolesCollection = mongoDb
            .GetCollection<Role>(RolesCollectionName)
            .AsQueryable();
    }

    public async Task<bool> ValidateUserEmailExist(string email)
    {
        var userQuery = from u in this._usersCollection
                        where u.Email == email
                        select u;

        var user = await userQuery.FirstOrDefaultAsync();

        if (user == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> ValidateUser(string id, IEnumerable<string> roleNames)
    {
        var userRoleIdsQuery = from u in this._usersCollection
                               where u.Id == id
                               from r in u.Roles
                               select r;

        var userRoleId = userRoleIdsQuery.FirstOrDefault();

        if (userRoleId == null)
        {
            return await Task.FromResult(false);
        }

        var userRoleNameQuery = from r in this._rolesCollection
                                where r.Id == userRoleId
                                select r.Name;

        var userRoleName = userRoleNameQuery.FirstOrDefault();

        if (!roleNames.Contains(userRoleName))
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }
}
