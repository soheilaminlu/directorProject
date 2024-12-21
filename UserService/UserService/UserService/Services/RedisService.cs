using StackExchange.Redis;
using UserService.Data;
using UserService.Interface;

namespace UserService.Services
{
    public class RedisService : IRedisService
    {
        private readonly IJwtService _jwtService;
        private readonly IConnectionMultiplexer _redisConnection;

        public RedisService(IJwtService jwtService , IConnectionMultiplexer redisConnection)
        {
           _jwtService = jwtService;
            _redisConnection = redisConnection;
        }

        public async Task DeleteRefreshTokenAsync(string key)
        {
            var db = _redisConnection.GetDatabase();
            await db.KeyDeleteAsync(key);
        }

        public async Task<string> GetRefreshTokenAsync(string key)
        {
            var db = _redisConnection.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task  SaveRefreshTokenAsync(string email , string token)
        {
            var db = _redisConnection.GetDatabase();
            var key = email;
            await db.StringSetAsync(key, token);
        }
    }
}
