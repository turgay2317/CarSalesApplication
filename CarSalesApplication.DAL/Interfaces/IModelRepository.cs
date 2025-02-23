using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.DAL.Interfaces;

public interface IModelRepository
{
    public Model? GetById(int id);
}