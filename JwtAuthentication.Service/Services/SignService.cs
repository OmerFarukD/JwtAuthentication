using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthentication.Service.Services;

public static class SignService
{
    public static SecurityKey GetSymmetricSecurityKey(string securityKey) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    
}