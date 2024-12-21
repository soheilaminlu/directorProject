using System.Security.Claims;

namespace UserService.Interface
{
    public interface IJwtService
    {
        string GenerateToken(string email);
        string GenerateRefreshToken(string email);
        IEnumerable<Claim> GetTokenClaim(string token);
    }
}
