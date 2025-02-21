using AutoMapper;
using CarSalesApplication.BLL.DTOs.Responses.Model;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarSalesApplication.BLL.Services;

public class ModelService : IModelService
{
    private readonly IModelRepository _modelRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ModelService> _logger;
    private readonly IMemoryCacheService _memoryCacheService;
    
    public ModelService(
        IModelRepository modelRepository, 
        IMapper mapper, 
        ILogger<ModelService> logger, 
        IMemoryCacheService memoryCacheService)
    {
        _modelRepository = modelRepository;
        _mapper = mapper;
        _logger = logger;
        _memoryCacheService = memoryCacheService;
    }
    
    public async Task<ModelDtoWithCars> GetModelByIdAsync(int id)
    {
        _logger.LogInformation("{ModelId} idli modele ait arabalar için istek geldi", id);
        // Retrieving from In-Memory cache
        var modelDtoWithCars = await _memoryCacheService.GetAsync<ModelDtoWithCars>($"Model/{id}");
        if (modelDtoWithCars != null)
            return modelDtoWithCars;
        
        // Retrieving from database
        modelDtoWithCars = new ModelDtoWithCars();
        
        try
        {
            var modelWithCars = await _modelRepository.GetByIdAsync(id);
            modelDtoWithCars = _mapper.Map<ModelDtoWithCars>(modelWithCars);
            _logger.LogInformation("{ModelId} id için arabalar getirildi.", id);
            await _memoryCacheService.SetAsync($"Model/{id}", modelDtoWithCars, TimeSpan.FromHours(12));
        }
        catch (AutoMapperMappingException e)
        {
            _logger.LogError("{ModelId} id için maplemede sorun oluştu. {Message}", id, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError("{ModelId} id için bir sorun oluştu. {Message}", id, e.Message);
        }

        return modelDtoWithCars;
    }
}