using TeamService.Models;

namespace TeamService.Services.TeamsService;

public interface ITeamsService
{
    Task<Result<string>> Create(string name, string goals, IEnumerable<string> members);
}
