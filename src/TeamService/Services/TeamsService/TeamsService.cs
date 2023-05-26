using TeamService.Infrastructure.Extensions;
using TeamService.Models;
using TeamService.Models.Data;
using MongoDB.Driver;

using static TeamService.Infrastructure.Validations.ExceptionMessages;
using TeamService.Models.Responses;
using MongoDB.Bson;

namespace TeamService.Services.TeamsService;

public class TeamsService : ITeamsService
{
    private const string TeamsCollectionName = "Teams";
    private const string UsersCollectionName = "Users";

    private readonly IMongoCollection<Team> _teamsCollection;
    private readonly IMongoCollection<User> _usersCollection;

    public TeamsService(IConfiguration configuration)
    {
        var mongoDbConfiguration = configuration.GetMongoDbConfigurations();

        var mongoClient = new MongoClient(mongoDbConfiguration.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(mongoDbConfiguration.Name);

        this._teamsCollection = mongoDb
            .GetCollection<Team>(TeamsCollectionName);

        this._usersCollection = mongoDb
            .GetCollection<User>(UsersCollectionName);
    }

    public async Task<IEnumerable<TeamListResponseModel>> GetAll()
        => await this._teamsCollection
            .Find(_ => true)
            .Project(t => new TeamListResponseModel
            {
                Name = t.Name,
                Goals = t.Goals,
                MembersUsernames = t.Members
            })
            .ToListAsync();

    public async Task<Result<string>> Create(string name, string goals, IEnumerable<string> members)
    {
        var result = new Result<string>();

        var teamExist = await this._teamsCollection
            .Find(t => t.Name == name)
            .AnyAsync();

        if (teamExist)
        {
            result.AddErrors(TeamWithTheSameNameExceptionMessage);
            return result;
        }

        var membersFilter = Builders<User>.Filter.In(u => u.Username, members);

        var membersUsernames = await this._usersCollection
            .Find(membersFilter)
            .Project(m => m.Username)
            .ToListAsync();

        if (membersUsernames.Count != members.Count())
        {
            result.AddErrors(InvalidUsernameExceptionMessage);
            return result;
        }

        var newTeam = new Team
        {
            Name = name,
            Goals = goals,
            Members = membersUsernames
        };

        await this._teamsCollection.InsertOneAsync(newTeam);

        result.Data = newTeam.Id;

        return result;
    }
}
