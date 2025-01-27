using CarSalesApplication.BLL.DTOs.Responses;

namespace CarSalesApplication.BLL.Interfaces;

public interface IModelService
{
    public Task<ModelDto> GetModelByIdAsync(int id);
}