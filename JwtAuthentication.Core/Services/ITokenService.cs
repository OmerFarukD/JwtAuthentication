using JwtAuthentication.Core.Configuration;
using JwtAuthentication.Core.Dtos;
using JwtAuthentication.Core.Model;

namespace JwtAuthentication.Core.Services;

public interface ITokenService
{
    TokenDto CreateToken(UserApp userApp);
    ClientTokenDto CreateByClient(Client client);
}