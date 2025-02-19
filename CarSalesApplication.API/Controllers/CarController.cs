using System.Security.Claims;
using CarSalesApplication.BLL.DTOs.Requests.Car;
using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.BLL.Services;
using CarSalesApplication.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApplication.Presentation.Controllers;

[Authorize]
[Route("api/car")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IElasticSearchService _elasticSearchService;
    private readonly ILogger<CarController> _logger;
    private readonly IRedisCacheService _redisCacheService;
    public CarController(
        ICarService carService, 
        IElasticSearchService elasticsearchService, 
        ILogger<CarController> logger,
        IRedisCacheService redisCacheService
        ) {
        _carService = carService;
        _elasticSearchService = elasticsearchService;
        _logger = logger;
        _redisCacheService = redisCacheService;
    }
    
    //[Authorize(Policy = "User")]
    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<List<CarDto>> GetAllCars([FromQuery] PostStatus? type)
    { 
        _logger.LogInformation("{PostType} tipi arabalar için istek geldi", type);
        return await _redisCacheService.GetCarsAsync(type);
    }
    
    // TODO: Cronjob bağla. Belli aralıklarla veritabanına set edilsin.
    [AllowAnonymous]
    [HttpGet("all/set")]
    public async Task<IActionResult> SetAllCars([FromQuery] PostStatus? type)
    { 
        _logger.LogInformation("{PostType} tipi arabalar cache belleğe kaydedilecek.", type);
        var cars = await _carService.GetAllCarsAsync(type);
        await _redisCacheService.SetCarsAsync(type, cars);
        _logger.LogInformation("{PostType} tipi arabalar cache belleğe kaydedildi.", type);
        return Ok(cars);
    }
    
    // TODO: Endpointi ve işlemleri düzenle.
    [AllowAnonymous]
    [HttpGet("all/set/es")]
    public async Task<IActionResult> SetAllCarsElasticSearch([FromQuery] PostStatus? type)
    { 
        _logger.LogInformation("{PostType} tipi arabalar elastic searche kaydedilecek.", type);
        var cars = await _carService.GetAllCarsAsync(type);
        await _elasticSearchService.AddOrUpdateBulk(cars, "cars");
        _logger.LogInformation("{PostType} tipi arabalar elastich searche kaydedildi.", type);
        return Ok(cars);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<CarDtoWithDetails> GetCarDetails(int id)
    {
        _logger.LogInformation("{CarId} IDli araba detayı için istek geldi.", id);
        return await _carService.GetCarDetailsAsync(id);
    }
    
    [AllowAnonymous]
    [HttpGet("/all/search")]
    public async Task<List<CarDto>> SearchCars([FromQuery] string keyword)
    {
        return await _elasticSearchService.GetAll(keyword);
    }

    // TODO: Yetkilendirmeyi ayarlar.
    // TODO: Araba eklenince elasticsearch indexini güncelle.
    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    public async Task<IActionResult> AddCar([FromBody] NewCarRequestDto request)
    {
        _logger.LogInformation("Yeni bir araba eklemek için istek geldi. {Request}", request);
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("You have to provide a user ID.");
        return Ok(await _carService.AddCarAsync(request, userId));
    }
}