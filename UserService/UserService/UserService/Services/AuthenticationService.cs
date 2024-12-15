using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Interface;
using UserService.Model;

namespace UserService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(UserManager<UserModel> userManager , IJwtService jwtService , ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _jwtService = jwtService; 
            _logger = logger;   
        }
        public async Task<UserModel> GetUserWithEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;    
        }

        public async Task<string> LoginAsync(string email, string password)
        {
           var existingUser = await GetUserWithEmailAsync(email).ConfigureAwait(false);
            if (existingUser == null || !await _userManager.CheckPasswordAsync(existingUser, password))
            {
                return null;
            }
            var token = _jwtService.GenerateToken(email, existingUser.Role.ToString());
            return token;
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> SignUpAsync(string username, string email, string password , RoleEnum role)
        {
            var user = new UserModel
           { 
                Email = email,  
                UserName = username,
                PasswordHash = password,
                Role = role 
           };
            var createUser = await _userManager.CreateAsync(user, password);
            if (!createUser.Succeeded)
            {
                _logger.LogError("User creation failed for {Username}", username);
                return null;
            }
            var token  = _jwtService.GenerateToken(user.Email , user.Role.ToString());
            return token;
        }
    }
}
