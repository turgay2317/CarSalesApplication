using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.BLL.Interfaces;

public interface ISearchService
{
    Task<bool> AddOrUpdate(CarDto car);
    Task<bool> AddOrUpdateBulk(List<CarDto> cars, string indexName);
    Task<List<CarDto>> GetAll(string keyword);
    
}