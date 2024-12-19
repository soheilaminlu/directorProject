using UserService.Configuration;
using UserService.Model;

namespace UserService.Interface
{
    public interface IAuthenticationService
    {
        Task<TokenModel> SignUpAsync(string username, string email, string password , RoleEnum role);
        Task<TokenModel> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<UserModel> GetUserWithEmailAsync(string email);
    }
}
