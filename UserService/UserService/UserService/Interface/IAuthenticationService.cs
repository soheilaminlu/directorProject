using UserService.Model;

namespace UserService.Interface
{
    public interface IAuthenticationService
    {
        Task<string> SignUpAsync(string username, string email, string password , RoleEnum role);
        Task<string> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<UserModel> GetUserWithEmailAsync(string email);
    }
}
