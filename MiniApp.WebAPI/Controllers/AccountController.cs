using System.Security.Claims;
using JwtAuthentication.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniApp.WebAPI.Models;
using MiniApp.WebAPI.Services;

namespace MiniApp.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AccountController : BaseController
{

    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("get")]
    public IActionResult Get()
    {
        var userName = HttpContext.User.Identity.Name;
        var userId = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
        
        return Ok($"User Id : {userId} -> UserName: {userName}");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailDto emailDto)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
        
        return ActionResultInstance(await _accountService.UpdateEmail(userId,emailDto.Email));
    }

}