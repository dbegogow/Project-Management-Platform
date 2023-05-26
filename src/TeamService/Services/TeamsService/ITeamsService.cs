using TeamService.Models;
using TeamService.Models.Responses;

namespace TeamService.Services.TeamsService;

public interface ITeamsService
{
    Task<IEnumerable<TeamListResponseModel>> GetAll();

    Task<Result<string>> Create(string name, string goals, IEnumerable<string> members);
}
