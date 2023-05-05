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

        var userRolesQuery = from u in usersQuerableCollection
                             where u.Id == id
                             from r in u.Roles
                             select r;

        var userRole = userRolesQuery.FirstOrDefault();

        var userRoleNameQuery = from r in rolesQuerableCollection
                                where r.Id == userRole
                                select r.Name;

        var userRoleName = userRoleNameQuery.FirstOrDefault();

        if (userRoleName != roleName)
        {
            return false;
        }

        return true;
    }
}
