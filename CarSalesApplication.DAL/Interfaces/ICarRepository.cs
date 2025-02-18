using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.DAL.Interfaces;

public interface ICarRepository
{
    public Task<List<Car>> GetAllCarsAsync(PostStatus? type);
    public Task<Car?> GetCarByIdAsync(int id);
    public Task<bool> AddCarAsync(Car car);
}