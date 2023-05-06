using TeamService.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

using static TeamService.Infrastructure.Constants.RoleConstants;

namespace TeamService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    public TeamsController()
    {
    }

    [HttpPost]
    [AuthorizeUser(AdminRole)]
    public async Task<IActionResult> Create()
    {
        return Ok();
    }
}
