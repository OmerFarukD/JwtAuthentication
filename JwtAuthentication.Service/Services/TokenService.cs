using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using JwtAuthentication.Core.Configuration;
using JwtAuthentication.Core.Dtos;
using JwtAuthentication.Core.Model;
using JwtAuthentication.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configuration;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace JwtAuthentication.Service.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<UserApp> _userManager;
    private readonly CustomTokenOptions _tokenOptions;

    public TokenService(UserManager<UserApp> userManager,IOptions<CustomTokenOptions> customOptions)
    {
        _userManager = userManager;
        _tokenOptions = customOptions.Value;
    }

    private string CreateRefreshToken()
    {
        var numberByte = new Byte[32];
        using var random =RandomNumberGenerator.Create();
        random.GetBytes(numberByte);

        return Convert.ToBase64String(numberByte);
    }

    private IEnumerable<Claim> GetClaim(UserApp userApp, List<string> audiences)
    {
        var userList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,userApp.Id),
            new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
            new Claim(ClaimTypes.Name,userApp.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString())
        };
        userList.AddRange(audiences.Select(x=>new Claim(JwtRegisteredClaimNames.Aud,x)));
        return userList;
    }

    private IEnumerable<Claim> GetClaimByClient(Client client)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub,client.Id.ToString())
        };
        claims.AddRange(client.Audience.Select(x=>new Claim(JwtRegisteredClaimNames.Aud,x)));
        return claims;
    }
    public TokenDto CreateToken(UserApp userApp)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
        var securityKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

        SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer:_tokenOptions.Issuer,
            expires:accessTokenExpiration,
            notBefore:DateTime.Now,
            claims:GetClaim(userApp,_tokenOptions.Audience),
            signingCredentials:signingCredentials);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new TokenDto
        {
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpiration,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration
        };
        return tokenDto;
    }

    public ClientTokenDto CreateByClient(Client client)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

        SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer:_tokenOptions.Issuer,
            expires:accessTokenExpiration,
            notBefore:DateTime.Now,
            claims:GetClaimByClient(client),
            signingCredentials:signingCredentials);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new ClientTokenDto()
        {
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpiration
        };
        return tokenDto;
    }
}