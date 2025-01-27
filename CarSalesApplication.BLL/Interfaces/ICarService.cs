using CarSalesApplication.BLL.DTOs.Requests;
using CarSalesApplication.BLL.DTOs.Responses;

namespace CarSalesApplication.BLL.Interfaces;

public interface ICarService
{
    public Task<List<CarDto>> GetCarsAsync();
    public Task<bool> AddCarAsync(InsertCarRequestDto request, string userId);
}