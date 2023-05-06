namespace TeamService.Services.TeamsService;

public interface ITeamsService
{
    Task<string> Create(string name, string goals, IEnumerable<string> members);
}
