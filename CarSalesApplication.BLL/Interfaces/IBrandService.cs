using CarSalesApplication.BLL.DTOs.Responses;

namespace CarSalesApplication.BLL.Interfaces;

public interface IBrandService
{
    Task<List<BrandDto>> GetBrandsAsync();
    Task<BrandDto> GetBrandByIdAsync(int brandId);
}