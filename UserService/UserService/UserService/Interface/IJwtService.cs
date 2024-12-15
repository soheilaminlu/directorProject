namespace UserService.Interface
{
    public interface IJwtService
    {
        string GenerateToken(string email, string role);
    }
}
