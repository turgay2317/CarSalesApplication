using CarSalesApplication.BLL.DTOs.Requests.Car;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.DTOs.Responses.Car;

namespace CarSalesApplication.BLL.Interfaces;

public interface ICarService
{
    public Task<List<CarDto>> GetCarsAsync();
    public Task<bool> AddCarAsync(NewCarRequestDto request, string userId);
}