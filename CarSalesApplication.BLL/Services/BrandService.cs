using AutoMapper;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;

namespace CarSalesApplication.BLL.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    
    public BrandService(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }
    
    public async Task<List<BrandDto>> GetBrandsAsync()
    {
        var brands = await _brandRepository.GetBrandsAsync();
        return _mapper.Map<List<BrandDto>>(brands);
    }

    public async Task<BrandDto> GetBrandByIdAsync(int brandId)
    {
        return _mapper.Map<BrandDto>(await _brandRepository.GetBrandByIdAsync(brandId));
    }
}