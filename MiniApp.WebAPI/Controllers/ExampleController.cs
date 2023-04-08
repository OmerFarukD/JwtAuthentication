using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiniApp.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{

    [HttpGet("get")]
    public IActionResult Get()
    {
        var userName = HttpContext.User.Identity.Name;
        var userId = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier));
        
        return Ok($"User Id : {userId} -> UserName: {userName}");
    }
    
}