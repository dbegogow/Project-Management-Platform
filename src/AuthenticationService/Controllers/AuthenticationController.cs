using AuthenticationService.Models.Data;
using AuthenticationService.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using static AuthenticationService.Infrastructure.Constants.ErrorMessageConstants;
using static AuthenticationService.Infrastructure.Constants.RoleConstants;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public AuthenticationController(
        UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
    }

    [HttpPost]
    [Route(nameof(Register))]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        var role = await this._roleManager.RoleExistsAsync(model.Role);

        if (!role)
        {
            return BadRequest(NotExistingRole);
        }

        var user = new User
        {
            Email = model.Email,
            UserName = model.Username,
        };

        var result = await this._userManager.CreateAsync(user, model.Password);

        await this._userManager.AddToRoleAsync(user, model.Role);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}
