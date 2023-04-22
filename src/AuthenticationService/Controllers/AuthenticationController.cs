using AuthenticationService.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        throw new NotImplementedException();
    }
}
