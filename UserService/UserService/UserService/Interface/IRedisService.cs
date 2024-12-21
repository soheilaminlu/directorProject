namespace UserService.Interface
{
    public interface IRedisService
    {
        Task SaveRefreshTokenAsync(string email , string token);
        Task<string> GetRefreshTokenAsync(string key);
        Task DeleteRefreshTokenAsync(string key);
    }
}
