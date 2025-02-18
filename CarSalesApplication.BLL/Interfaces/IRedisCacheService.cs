using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.Core.Enums;

namespace CarSalesApplication.BLL.Interfaces;

public interface IRedisCacheService
{
    Task<string> GetValueAsync(string key);
    Task<bool> SetValueAsync(string key, string value);
    Task Clear(string key);  
    void ClearAll(); 
    Task<bool> SetCarsAsync(PostStatus? type, List<CarDto> cars);
    Task<List<CarDto>?> GetCarsAsync(PostStatus? type);
}