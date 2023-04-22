using AuthenticationService.Models.Data;
using AuthenticationService.Models.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public AuthenticationController(UserManager<User> userManager)
    {
        this._userManager = userManager;
    }

    [HttpPost]
    [Route(nameof(Register))]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        var user = new User
        {
            Email = model.Email,
            UserName = model.Username,
        };

        var result = await this._userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}
