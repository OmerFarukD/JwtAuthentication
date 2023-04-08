using JwtAuthentication.Core.Dtos;
using JwtAuthentication.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("createuser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
    }

    [HttpGet("getbyusername")]
    [Authorize]
    public async Task<IActionResult> GetByUserName()
    {
        return ActionResultInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
    }
    
}