using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Configuration;
using UserService.Data;
using UserService.Interface;
using UserService.Model;

namespace UserService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IRedisService _redisService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(UserManager<UserModel> userManager , IJwtService jwtService , 
            ILogger<AuthenticationService> logger , IRedisService redisService)
        {
            _redisService = redisService;
            _userManager = userManager;
            _jwtService = jwtService; 
            _logger = logger;   
        }
        public async Task<UserModel> GetUserWithEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;    
        }

        public async Task<TokenModel> LoginAsync(string email, string password)
        {
           var existingUser = await GetUserWithEmailAsync(email).ConfigureAwait(false);
            if (existingUser == null || !await _userManager.CheckPasswordAsync(existingUser, password))
            {
                return null;
            }
            var token = _jwtService.GenerateToken(email, existingUser.Role.ToString());
            var refreshToken = _jwtService.GenerateRefreshToken(token);
            await _redisService.SaveRefreshTokenAsync(refreshToken , email);
            return new TokenModel
            {
                AccessToken = token,
                refreshToken = refreshToken,
            };
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TokenModel> SignUpAsync(string username, string email, string password , RoleEnum role)
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
            var refreshToken = _jwtService.GenerateRefreshToken(email);
             await _redisService.SaveRefreshTokenAsync(refreshToken, email);
            return new TokenModel
            {
                AccessToken = token,
                refreshToken = refreshToken,
            };
        }
    }
}
