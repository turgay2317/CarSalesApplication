using CarSalesApplication.BLL.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CarSalesApplication.BLL.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<bool> SetAsync<T>(string key, T value, TimeSpan? duration = null)
        {
            var expiration = duration ?? TimeSpan.FromMinutes(10);
            _memoryCache.Set(key, value, expiration);
            return Task.FromResult(true);
        }

        public Task<T> GetAsync<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T value);
            return Task.FromResult(value);
        }
    }
}