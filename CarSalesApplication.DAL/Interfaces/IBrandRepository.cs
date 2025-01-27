using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.DAL.Interfaces;

public interface IBrandRepository
{
    Task<List<Brand>> GetBrandsAsync();
    Task<Brand?> GetBrandByIdAsync(int brandId);
}