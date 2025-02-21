using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.Core.Enums;

namespace CarSalesApplication.BLL.Interfaces;

public interface IRedisCacheService
{
    Task<string> GetValueAsync(string key);
    Task<bool> SetValueAsync(string key, string value);
    Task Clear(string key);  
    void ClearAll(); 
    // Cars
    Task<bool> SetCarsAsync(PostStatus? type, List<CarDto> cars);
    Task<List<CarDto>?> GetCarsAsync(PostStatus? type);
    // Single Car Details
    Task<bool> SetCarAsync(string key, CarDtoWithDetails car);
    Task<CarDtoWithDetails?> GetCarAsync(string key);
}