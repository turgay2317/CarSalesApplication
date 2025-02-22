using AutoMapper;
using CarSalesApplication.BLL.DTOs.Requests.Car;
using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarSalesApplication.BLL.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CarService> _logger;
    private readonly ICacheService _cacheService;

    public CarService(
        ICarRepository carRepository, 
        IPhotoRepository photoRepository, 
        IMapper mapper, 
        [FromKeyedServices("Redis")] ICacheService cacheService,
        ILogger<CarService> logger)
    {
        _carRepository = carRepository;
        _photoRepository = photoRepository;
        _mapper = mapper;
        _cacheService = cacheService;
        _logger = logger;
    }
    
    public async Task<List<CarDto>> GetAllCarsAsync(PostStatus? type)
    {
        string typeString = type.ToString().ToLower();
        _logger.LogInformation("{Type} tipinde arabalar talep edildi.", type);
        // Retrieving from redis cache
        var carDtos = await _cacheService.GetAsync<List<CarDto>>(typeString);
        if (carDtos != null)
            return carDtos;
        
        // Retrieving from database
        carDtos = new List<CarDto>();
        
        try
        {
            var cars = await _carRepository.GetAllCarsAsync(type);
            carDtos = _mapper.Map<List<CarDto>>(cars);
            _logger.LogInformation("{Type} tipinde arabalar getirildi.", type);
            await _cacheService.SetAsync<List<CarDto>>(typeString, carDtos);
        }
        catch (AutoMapperMappingException e)
        {
            _logger.LogError("{Type} tipinde arabalar getirilirken mapleme işleminde hata oluştu: {Message}", type, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError("{Type} tipinde arabalar getirilirken beklenmedik bir hata Oluştu! {Message}", type, e.Message);
        }

        return carDtos;
    }
    
    public async Task<CarDtoWithDetails> GetCarDetailsAsync(int carId)
    {
        _logger.LogInformation("{CarId} idli arabanın detayı talep edildi.", carId);
        
        // Retrieving from redis cache
        var carDto = await _cacheService.GetAsync<CarDtoWithDetails>($"car/{carId}");
        if (carDto != null)
            return carDto;
        
        // Retrieving from database
        carDto = new CarDtoWithDetails();
        
        try
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            carDto = _mapper.Map<CarDtoWithDetails>(car);
            await _cacheService.SetAsync<CarDtoWithDetails>($"car/{carId}", carDto);
        }
        catch (AutoMapperMappingException e)
        {
            _logger.LogError("{CarId} idli araba detayı getirilirken mapleme işleminde hata oluştu: {Message}", carId, e.ToString());
        }
        catch (Exception e)
        {
            _logger.LogError("{CarId} idli araba detayı getirilirken beklenmedik bir hata oluştu: {Message}", carId, e.ToString());
        }

        return carDto;
    }
    
    // TODO: Araba eklenmesi tam implemente edilecek.
    public async Task<bool> AddCarAsync(NewCarRequestDto request, string userId)
    {
        Car newCar = new Car()
        {
            BrandId = request.BrandId,
            ModelId = request.ModelId,
            Color = request.Color,
            Year = request.Year,
            Price = request.Price,
            Status = PostStatus.Pending,
            CreatedAt = DateTime.Now,
            UserId = int.Parse(userId)
        };

        await _carRepository.AddCarAsync(newCar);

        var photoEntities = request.Photos.Select(photoDto => new Photo()
        {
            CarId = newCar.Id,
            Data = photoDto.Data
        });

        await _photoRepository.SavePhotosAsync(photoEntities);

        return true;
    }
}