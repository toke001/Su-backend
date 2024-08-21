using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface IAccount
    {
        public Task<AccountSignInResponseDto> SignIn(AccountSignInRequestDto request);
        Task<AccountSignUpResponseDto> SignUp(AccountSignUpRequestDto request);
        Task<object> GetUsers(AccountGetUsersRequestDto model);
    }
}
