using SharedLibrary.Dtos;

namespace MiniApp.WebAPI.Services;

public interface IAccountService
{
    Task<Response<NoDataDto>> UpdateEmail(string userId, string email);
}