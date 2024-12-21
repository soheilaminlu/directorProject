using UserService.Configuration;
using UserService.Dto;
using UserService.Model;

namespace UserService.Interface
{
    public interface IAuthService
    {
        Task<TokenModel> SignUpAsync(SignupDto signupDto);
        Task<TokenModel> LoginAsync(LoginDto loginDto);
        Task LogoutAsync(string email);
        Task<UserModel> GetUserWithEmailAsync(string email);
    }
}
