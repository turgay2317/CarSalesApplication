using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.DAL.Interfaces;

public interface IBrandRepository
{
    List<Brand> GetBrands();
    Brand? GetBrandById(int brandId);
}