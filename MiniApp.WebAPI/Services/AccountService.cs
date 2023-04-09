using JwtAuthentication.Core.Model;
using JwtAuthentication.Data;
using Microsoft.AspNetCore.Identity;
using MiniApp.WebAPI.Models;
using SharedLibrary.Dtos;

namespace MiniApp.WebAPI.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<AccountEntity> _userManager;

    public AccountService(UserManager<AccountEntity> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Response<NoDataDto>> UpdateEmail(string userId, string email)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if(user is null) return Response<NoDataDto>.Fail("User is not found",400,true);

        user.Email = email;

        await _userManager.UpdateAsync(user);
        
        return Response<NoDataDto>.Success(200);
    }
}