using AutoMapper;
using CarSalesApplication.BLL.DTOs.Requests.Car;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;

namespace CarSalesApplication.BLL.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;

    public CarService(ICarRepository carRepository, IPhotoRepository photoRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _photoRepository = photoRepository;
        _mapper = mapper;
    }
    
    public async Task<List<CarDto>> GetCarsAsync()
    {
        var cars = await _carRepository.GetCarsAsync();
        return _mapper.Map<List<CarDto>>(cars);
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
            Status = PostType.Pending,
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