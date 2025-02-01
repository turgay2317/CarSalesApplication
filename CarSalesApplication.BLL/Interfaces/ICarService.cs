using CarSalesApplication.BLL.DTOs.Requests.Car;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.Core.Enums;

namespace CarSalesApplication.BLL.Interfaces;

public interface ICarService
{
    public Task<List<CarDto>> GetAllCarsAsync(PostType? type);
    public Task<bool> AddCarAsync(NewCarRequestDto request, string userId);
    public Task<CarDtoWithProfile> GetCarDetailsAsync(int carId);
}