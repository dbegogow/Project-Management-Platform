﻿using System.ComponentModel.DataAnnotations;
using AuthenticationService.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using static AuthenticationService.Infrastructure.Constants.RoleConstants;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly RoleManager<Role> _roleManager;

    public RolesController(RoleManager<Role> roleManager)
    {
        this._roleManager = roleManager;
    }

    [HttpPost]
    [Route(nameof(Create))]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Create([FromBody][Required] string roleName)
    {
        var result = await this._roleManager.CreateAsync(new Role() { Name = roleName });

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return Ok();
    }
}
