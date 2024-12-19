namespace UserService.Interface
{
    public interface IRedisService
    {
        Task SaveRefreshTokenAsync(string token, string email);
    }
}
