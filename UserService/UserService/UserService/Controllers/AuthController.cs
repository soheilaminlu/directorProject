using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Dto;
using UserService.Interface;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;
        private readonly IRedisService _redisService;
        public AuthController(IAuthService authService , IJwtService jwtService, IRedisService redisService)
        {
            _authService = authService;
            _jwtService = jwtService;
            _redisService = redisService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpUser([FromBody] SignupDto signupDto)
        {
            var createUser = await _authService.SignUpAsync(signupDto);
            if (createUser == null)
            {
                return BadRequest($"Failed to SignUp User with this email {signupDto.Email}");
            }
            return Ok(createUser);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            var loginUser = await _authService.LoginAsync(loginDto);
            if (loginUser == null)
            {
                return Unauthorized($"unAuthorized user with email {loginDto.Email}");
            }
            return Ok(loginUser);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("No access token provided");
            }
            var tokenClaim = _jwtService.GetTokenClaim(accessToken);
            var email =  tokenClaim.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            if (email != null)
            {
                // حذف توکن refresh از Redis
                await _redisService.DeleteRefreshTokenAsync(email);
            }
            return Ok(email);
        }
    }
}
