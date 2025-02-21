using System.Text.Json;
using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.Core.Enums;
using StackExchange.Redis;

namespace CarSalesApplication.BLL.Services;

public class RedisCacheService : IRedisCacheService
{
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly IDatabase _cache;

    public RedisCacheService(IConnectionMultiplexer redisConnection)
    {
        _redisConnection = redisConnection; 
        _cache = redisConnection.GetDatabase();
    }

    public async Task<bool> SetValueAsync(string key, string value)
    {
        return await _cache.StringSetAsync(key, value, TimeSpan.FromMinutes(10));
    }

    public async Task<string> GetValueAsync(string key)
    {
        return await _cache.StringGetAsync(key);
    }

    public async Task Clear(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }

    public void ClearAll()
    {
        var redisEndpoints = _redisConnection.GetEndPoints(true);
        foreach (var redisEndpoint in redisEndpoints)
        {
            var redisServer = _redisConnection.GetServer(redisEndpoint);
            redisServer.FlushAllDatabases();
        }
    }

    
    public async Task<bool> SetCarsAsync(PostStatus? type, List<CarDto> cars)
    {
        string key = type != null ? type.ToString().ToLower() : "all";
        var carsJson = JsonSerializer.Serialize(cars);
        return await SetValueAsync(key, carsJson); 
    }

    public async Task<List<CarDto>?> GetCarsAsync(PostStatus? type)
    {
        string key = type != null ? type.ToString().ToLower() : "all";
        var carsJson = await GetValueAsync(key);
        if (string.IsNullOrEmpty(carsJson))
        {
            return null;
        }

        return JsonSerializer.Deserialize<List<CarDto>>(carsJson);
    }

    public async Task<bool> SetCarAsync(string key, CarDtoWithDetails car)
    {
        return await SetValueAsync(key, JsonSerializer.Serialize(car));
    }

    public async Task<CarDtoWithDetails?> GetCarAsync(string key)
    {
        var carJson = await GetValueAsync(key);
        if (string.IsNullOrEmpty(carJson))
        {
            return null;
        }        
        return JsonSerializer.Deserialize<CarDtoWithDetails>(carJson);
    }
}