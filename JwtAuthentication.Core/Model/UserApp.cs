using Microsoft.AspNetCore.Identity;

namespace JwtAuthentication.Core.Model;

public class UserApp : IdentityUser
{
    public string? City { get; set; }
}