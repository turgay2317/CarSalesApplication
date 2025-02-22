using AutoMapper;
using CarSalesApplication.BLL.DTOs.Responses.Brand;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarSalesApplication.BLL.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<BrandService> _logger;
    private readonly ICacheService _cacheService;
    
    public BrandService(
        IBrandRepository brandRepository, 
        IMapper mapper, 
        ILogger<BrandService> logger,
        [FromKeyedServices("Memory")] ICacheService cacheService)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
        _logger = logger;
        _cacheService = cacheService;
    }
    
    public async Task<List<BrandDto>> GetBrandsAsync()
    {
        _logger.LogInformation("Markalar için istek geldi.");
        // Retrieving from In-Memory Cache
        var brandsDto = await _cacheService.GetAsync<List<BrandDto>>("Brands");
        if (brandsDto != null)
            return brandsDto;
        
        // Retrieving from database
        brandsDto = new List<BrandDto>();
        
        try
        {
            var brands = await _brandRepository.GetBrandsAsync();
            brandsDto = _mapper.Map<List<BrandDto>>(brands);
            _logger.LogInformation("Markalar getirildi");
            await _cacheService.SetAsync("Brands", brandsDto, TimeSpan.FromDays(1));
        }
        catch (AutoMapperMappingException e)
        {
            _logger.LogInformation("Markalar getirilirken mapping hatası {Message}", e.Message);
        }
        catch (Exception e)
        {
            _logger.LogInformation("Markalar getirilirken beklenmedik hata {Message}", e.Message);
        }

        return brandsDto;
    }

    public async Task<BrandDtoWithModels> GetBrandByIdAsync(int brandId)
    {
        _logger.LogInformation("{BrandId} idli  marka için model listeleme isteği geldi.", brandId);
        // Retrieving from In-Memory Cache
        var brandDtoWithModels = await _cacheService.GetAsync<BrandDtoWithModels>($"Brands/{brandId}");
        if (brandDtoWithModels != null)
            return brandDtoWithModels;
        
        // Retrieving from database
        brandDtoWithModels = new BrandDtoWithModels();
        
        try
        {
            var brandWithModels = await _brandRepository.GetBrandByIdAsync(brandId);
            brandDtoWithModels = _mapper.Map<BrandDtoWithModels>(brandWithModels);
            _logger.LogInformation("{BrandId} idli  marka için modeller listelendi.", brandId);
            await _cacheService.SetAsync($"Brands/{brandId}", brandWithModels, TimeSpan.FromDays(1));
        }
        catch (AutoMapperMappingException e)
        {
            _logger.LogInformation("{BrandId} idli  marka için modeller getirilirken mapping hatası {Message}", brandId, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogInformation("{BrandId} idli  marka için modeller getirilirken beklenmedik hata {Message}", brandId, e.Message);
        }
        return brandDtoWithModels;
    }
}