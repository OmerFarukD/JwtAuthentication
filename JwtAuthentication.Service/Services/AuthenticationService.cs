using JwtAuthentication.Core.Configuration;
using JwtAuthentication.Core.Dtos;
using JwtAuthentication.Core.Model;
using JwtAuthentication.Core.Repository;
using JwtAuthentication.Core.Services;
using JwtAuthentication.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.Dtos;

namespace JwtAuthentication.Service.Services;

public class AuthenticationService: IAuthenticationService
{

    private readonly ITokenService _tokenService;
    private readonly List<Client> _clients;
    private readonly UserManager<UserApp> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<UserRefreshToken> _serviceGeneric;

    public AuthenticationService(ITokenService tokenService, IOptions<List<Client>> clients, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> serviceGeneric)
    {
        _tokenService = tokenService;
        _clients = clients.Value;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _serviceGeneric = serviceGeneric;
    }

    public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
    {
        if (loginDto is null) throw new ArgumentNullException(nameof(loginDto));

        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        
        if(user is null) return Response<TokenDto>.Fail("Email or password is wrong",400,true);

        if (!await _userManager.CheckPasswordAsync(user,loginDto.Password)) return Response<TokenDto>.Fail("Email or password is wrong",400,true);

        var token = _tokenService.CreateToken(user);
        var userRefreshToken =await _serviceGeneric.Where(x=>x.UserId.Equals(user.Id)).SingleOrDefaultAsync();


        if (userRefreshToken is null) await _serviceGeneric.AddAsync(new UserRefreshToken{UserId = user.Id,Code = token.RefreshToken,Expiration = token.RefreshTokenExpiration});
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
            userRefreshToken.UserId = user.Id;
        }

        await _unitOfWork.SaveChangesAsync();
        
        return Response<TokenDto>.Success(token,200);
    }

    public async Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<ClientTokenDto>> CreateTokenByClientAsync(ClientLoginDto clientLoginDto)
    {
        throw new NotImplementedException();
    }
}