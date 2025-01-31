using CarSalesApplication.BLL.DTOs.Responses.Brand;

namespace CarSalesApplication.BLL.Interfaces;

public interface IBrandService
{
    Task<List<BrandDto>> GetBrandsAsync();
    Task<BrandDtoWithModels> GetBrandByIdAsync(int brandId);
}