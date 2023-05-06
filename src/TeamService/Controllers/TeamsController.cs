using TeamService.Infrastructure.Attributes;
using TeamService.Models.Requests;
using TeamService.Services.TeamsService;
using Microsoft.AspNetCore.Mvc;

using static TeamService.Infrastructure.Constants.RoleConstants;

namespace TeamService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamsService _teamsService;

    public TeamsController(ITeamsService teamsService)
        => this._teamsService = teamsService;

    [HttpPost]
    [AuthorizeUser(AdminRole, ManagerRole)]
    public async Task<IActionResult> Create([FromBody] CreateTeamRequestModel model)
    {
        var result = await this._teamsService
            .Create(model.Name, model.Goals, model.Members);

        if (result.Failure)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }
}
