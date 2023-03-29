using JwtAuthentication.Core.Dtos;
using SharedLibrary.Dtos;

namespace JwtAuthentication.Core.Services;

public interface IUserService
{
    Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto);
    Task<Response<UserDto>> GetUserByNameAsync(string name);
}