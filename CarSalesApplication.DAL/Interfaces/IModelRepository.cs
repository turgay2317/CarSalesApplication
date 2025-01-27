using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.DAL.Interfaces;

public interface IModelRepository
{
    public Task<Model?> GetByIdAsync(int id);
}