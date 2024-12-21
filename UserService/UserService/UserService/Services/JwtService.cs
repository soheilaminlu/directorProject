using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Configuration;
using UserService.Interface;

namespace UserService.Services;

    public class JwtService : IJwtService
    {
    private readonly JwtModel _jwtModel;
    public JwtService(IOptions<JwtModel> jwtModel)
    {
        _jwtModel = jwtModel.Value;
    }
    public string GenerateToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtModel.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
           {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email , email),
            };
        var token = new JwtSecurityToken(
               issuer: _jwtModel.Issuer,
               audience: _jwtModel.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_jwtModel.ExpiryInMinutes),
               signingCredentials: credentials
           );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateRefreshToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtModel.SecretKey));    
        var credentials = new SigningCredentials(securityKey , SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
           new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email , email),
        };
        var token = new JwtSecurityToken(
               issuer: _jwtModel.Issuer,
               audience: _jwtModel.Audience,
               claims: claims,
               expires: DateTime.Now.AddMonths(_jwtModel.ExpiryInMonth),
               signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public IEnumerable<Claim> GetTokenClaim(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken =  handler.ReadJwtToken(token);
        return jwtToken?.Claims;
    }
}

