using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BCrypt;
using UserService.Configuration;
using UserService.Data;
using UserService.Interface;
using UserService.Model;
using UserService.Dto;
using BCrypt.Net;

namespace UserService.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IRedisService _redisService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ApplicationDbContext dbContext , IJwtService jwtService , 
            ILogger<AuthenticationService> logger , IRedisService redisService)
        {
            _redisService = redisService;
            _dbcontext = dbContext;
            _jwtService = jwtService; 
            _logger = logger;   
        }
        public async Task<UserModel> GetUserWithEmailAsync(string email)
        {
               var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.email ==  email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<TokenModel> LoginAsync(LoginDto loginUser)
        {
          
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.email ==  loginUser.Email);
            if (user == null)
            {
                return null;
            }
            var validatePassword = BCrypt.Net.BCrypt.Verify(loginUser.Password , user.password);
            if (!validatePassword)
            {
                return null;
            }
            var accessToken = _jwtService.GenerateToken(user.email);
            var refreshToken = _jwtService.GenerateRefreshToken(user.email);
            await _redisService.SaveRefreshTokenAsync(refreshToken, user.email);
            return new TokenModel
            {
                AccessToken = accessToken,
                refreshToken = refreshToken,
            };
        }

        public async Task LogoutAsync(string email)
        {
            await _redisService.DeleteRefreshTokenAsync(email);
        }

        public async Task<TokenModel> SignUpAsync(SignupDto createUserDto)
        {
            var exsitingUser = await GetUserWithEmailAsync(createUserDto.Email);
            if (exsitingUser != null)
            {
                throw new ArgumentException("user Already exist");
            }
            var user = new UserModel {
                email = createUserDto.Email,
                username = createUserDto.Username,
                password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password)
            };
            if (user == null)
            {
                return null;
            }
            var accessToken = _jwtService.GenerateToken(user.email);
            var refreshToken = _jwtService.GenerateRefreshToken(user.email);
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
            return new TokenModel
            {
                AccessToken = accessToken,
                refreshToken = refreshToken,
            };
        }
    }
}
