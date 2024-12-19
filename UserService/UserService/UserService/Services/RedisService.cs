using StackExchange.Redis;
using UserService.Data;
using UserService.Interface;

namespace UserService.Services
{
    public class RedisService : IRedisService
    {
        private readonly JwtService _jwtService;
        private readonly IConnectionMultiplexer _redisConnection;

        public RedisService(JwtService jwtService , IConnectionMultiplexer redisConnection)
        {
           _jwtService = jwtService;
            _redisConnection = redisConnection;
        }

        public async Task  SaveRefreshTokenAsync(string token , string email )
        {
            var db = _redisConnection.GetDatabase();
            var key = $"refresh_token:{email}";
            await db.StringSetAsync(key, token);
        }
    }
}
