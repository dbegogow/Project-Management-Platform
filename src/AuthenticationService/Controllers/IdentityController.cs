using AuthenticationService.Infrastructure.Attributes;
using AuthenticationService.Models.Data;
using AuthenticationService.Models.Request;
using AuthenticationService.Models.Response
using AuthenticationService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using static AuthenticationService.Infrastructure.Constants.ErrorMessageConstants;
using static AuthenticationService.Infrastructure.Constants.RoleConstants;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    private readonly IIdentityService _identityService;

    public IdentityController(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IIdentityService identityService)
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
        this._identityService = identityService;
    }

    [HttpPost]
    [Route(nameof(Register))]
    [AuthorizeRoles(AdminRole, ManagerRole)]
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

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await this._userManager.AddToRoleAsync(user, model.Role);

        var token = this._identityService.GenerateJwtToken(
               user.Id,
               user.UserName);

        return Ok(new IdentityResponseModel
        {
            Token = token
        });
    }
}
