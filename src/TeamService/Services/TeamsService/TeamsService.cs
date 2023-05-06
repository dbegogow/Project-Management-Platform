using TeamService.Infrastructure.Extensions;
using TeamService.Models.Data;
using MongoDB.Driver;

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

    public async Task<string> Create(string name, string goals, IEnumerable<string> members)
    {
        var newTeam = new Team
        {
            Name = name,
            Goals = goals,
            Members = members
        };

        await this._teamsCollection.InsertOneAsync(newTeam);

        return newTeam.Id;
    }
}
