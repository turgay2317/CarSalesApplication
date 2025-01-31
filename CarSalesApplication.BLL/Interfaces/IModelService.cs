using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.DTOs.Responses.Model;

namespace CarSalesApplication.BLL.Interfaces;

public interface IModelService
{
    public Task<ModelDtoWithCars> GetModelByIdAsync(int id);
}