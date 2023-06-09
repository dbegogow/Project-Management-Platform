﻿using System.ComponentModel.DataAnnotations;
using AuthenticationService.Infrastructure.Attributes;
using AuthenticationService.Infrastructure.Extensions;
using AuthenticationService.Models.Data;
using AuthenticationService.Models.Request;
using AuthenticationService.Models.Response;
using AuthenticationService.Services.Identity;
using AuthenticationService.Services.Publishing;
using AuthenticationService.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IUsersService _usersService;
    private readonly IPublishingService _publishingService;

    public IdentityController(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IIdentityService identityService,
        IUsersService usersService,
        IPublishingService publishingService)
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
        this._identityService = identityService;
        this._usersService = usersService;
        this._publishingService = publishingService;
    }

    [HttpPost]
    [Route(nameof(Register))]
    [AuthorizeRoles(AdminRole, ManagerRole)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
    {
        var role = await this._roleManager.RoleExistsAsync(model.Role);

        if (!role)
        {
            return BadRequest(NotExistingRole);
        }

        var emailExist = await this._usersService.ValidateUserEmailExist(model.Email);

        if (emailExist)
        {
            return BadRequest(EmailExist);
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

        await this._publishingService.PublishCreatedUser(user.Id, user.UserName);

        var token = this._identityService.GenerateJwtToken(
               user.Id,
               user.UserName,
               model.Role);

        return Ok(new IdentityResponseModel
        {
            Token = token
        });
    }

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
    {
        var user = await this._userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return Unauthorized();
        }

        var passwordValid = await this._userManager.CheckPasswordAsync(user, model.Password);

        if (!passwordValid)
        {
            return Unauthorized();
        }

        var role = (await this._userManager
            .GetRolesAsync(user))
            .FirstOrDefault();

        var token = this._identityService.GenerateJwtToken(
            user.Id,
            user.Email,
            role);

        return Ok(new IdentityResponseModel
        {
            Token = token
        });
    }

    [HttpGet]
    [Route(nameof(VerifyToken))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> VerifyToken([FromQuery][Required] IEnumerable<string> roles)
    {
        var userId = User.GetId();

        var result = await this._usersService
             .ValidateUser(userId, roles);

        if (!result)
        {
            return Unauthorized();
        }

        return Ok();
    }
}
