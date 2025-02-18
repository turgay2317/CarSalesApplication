using AutoMapper;
using CarSalesApplication.BLL.DTOs.Requests.Car;
using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarSalesApplication.BLL.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CarService> _logger;

    public CarService(ICarRepository carRepository, IPhotoRepository photoRepository, IMapper mapper, ILogger<CarService> logger)
    {
        _carRepository = carRepository;
        _photoRepository = photoRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<CarDto>> GetAllCarsAsync(PostStatus? type)
    {
        var carDtos = new List<CarDto>();
        try
        {
            var cars = await _carRepository.GetAllCarsAsync(type);
            carDtos = _mapper.Map<List<CarDto>>(cars);
            _logger.LogInformation("{Type} tipinde arabalar getirildi.", type);
        }
        catch (AutoMapperMappingException e)
        {
            _logger.LogError("Mapleme işleminde hata oluştu! {Message}", e.ToString());
        }
        catch (Exception e)
        {
            _logger.LogError("Hata Oluştu! {Message}", e.ToString());
        }

        return carDtos;
    }
    
    public async Task<CarDtoWithDetails> GetCarDetailsAsync(int carId)
    {
        var car = await _carRepository.GetCarByIdAsync(carId);
        return _mapper.Map<CarDtoWithDetails>(car);
    }
    
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