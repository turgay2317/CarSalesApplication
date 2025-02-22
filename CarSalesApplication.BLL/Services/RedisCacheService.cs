using System.Text.Json;
using StackExchange.Redis;
using CarSalesApplication.BLL.Interfaces;

namespace CarSalesApplication.BLL.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _cache;

        public RedisCacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            _cache = redisConnection.GetDatabase();
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? duration = null)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            var expiration = duration ?? TimeSpan.FromMinutes(10);
            return await _cache.StringSetAsync(key, serializedValue, expiration);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var valueJson = await _cache.StringGetAsync(key);
            if (string.IsNullOrEmpty(valueJson))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(valueJson);
        }
    }
}