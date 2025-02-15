using CarSalesApplication.BLL.DTOs.Responses.Car;
using PostType = CarSalesApplication.Core.Enums.PostType;

namespace CarSalesApplication.BLL.Interfaces;

public interface IRedisCacheService
{
    Task<string> GetValueAsync(string key);
    Task<bool> SetValueAsync(string key, string value);
    Task Clear(string key);  
    void ClearAll(); 
    Task<bool> SetCarsAsync(PostType? type, List<CarDto> cars);
    Task<List<CarDto>?> GetCarsAsync(PostType? type);
}