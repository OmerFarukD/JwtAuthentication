using JwtAuthentication.Core.Dtos;
using JwtAuthentication.Core.Model;
using JwtAuthentication.Core.Services;
using JwtAuthentication.Service.DtoMappers;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Dtos;

namespace JwtAuthentication.Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<UserApp> _userManager;

    public UserService(UserManager<UserApp> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new UserApp
        {
            Email = createUserDto.Email,
            UserName = createUserDto.UserName
        };
        var result = await _userManager.CreateAsync(user, createUserDto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();
            
            return Response<UserDto>.Fail(new ErrorDto(errors,true),400);
        }
        return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user),200);
    }

    public async Task<Response<UserDto>> GetUserByNameAsync(string name)
    {
        var user = await _userManager.FindByNameAsync(name);
        if(user is null) return Response<UserDto>.Fail("User not found",400,true);
        
        return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user),200);
    }
}