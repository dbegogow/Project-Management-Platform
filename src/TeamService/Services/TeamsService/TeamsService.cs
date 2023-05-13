using TeamService.Infrastructure.Extensions;
using TeamService.Models;
using TeamService.Models.Data;
using MongoDB.Driver;

using static TeamService.Infrastructure.Validations.ExceptionMessages;

namespace TeamService.Services.TeamsService;

public class TeamsService : ITeamsService
{
    private const string TeamsCollectionName = "Teams";

    private readonly IMongoCollection<Team> _teamsCollection;

    public TeamsService(IConfiguration configuration)
    {
        var mongoDbConfiguration = configuration.GetMongoDbConfigurations();

        var mongoClient = new MongoClient(mongoDbConfiguration.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(mongoDbConfiguration.Name);

        this._teamsCollection = mongoDb
            .GetCollection<Team>(TeamsCollectionName);
    }

    public async Task<Result<string>> Create(string name, string goals, IEnumerable<string> members)
    {
        var result = new Result<string>();

        var exist = await this._teamsCollection
            .Find(t => t.Name == name)
            .AnyAsync();

        if (exist)
        {
            result.AddErrors(TeamWithTheSameNameExceptionMessage);
            return result;
        }

        var newTeam = new Team
        {
            Name = name,
            Goals = goals,
            Members = members
        };

        await this._teamsCollection.InsertOneAsync(newTeam);

        result.Data = newTeam.Id;

        return result;
    }
}
