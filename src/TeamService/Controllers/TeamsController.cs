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

    public async Task<IActionResult> Create()
    {
        throw new NotImplementedException();
    }
}
