using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.DAL.Interfaces;

public interface ICarRepository
{
    public Task<List<Car>> GetCarsAsync();
    public Task<bool> AddCarAsync(Car car);
}