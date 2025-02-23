using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.DAL.Interfaces;

public interface ICarRepository
{
    public List<Car> GetAllCars(PostStatus? type);
    public Car? GetCarById(int id);
    public Task<bool> AddCarAsync(Car car);
}