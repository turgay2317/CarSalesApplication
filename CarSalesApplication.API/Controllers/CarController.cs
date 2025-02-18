using System.Security.Claims;
using CarSalesApplication.BLL.DTOs.Requests.Car;
using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.BLL.Services;
using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApplication.Presentation.Controllers;

[Authorize]
[Route("api/car")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly ElasticSearchService _elasticSearchService;
    private readonly ILogger<CarController> _logger;
    private readonly IRedisCacheService _redisCacheService;
    public CarController(
        ICarService carService, 
        ElasticSearchService elasticsearchService, 
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

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<CarDtoWithDetails> GetCarDetails(int id)
    {
        _logger.LogInformation("{CarId} IDli araba detayı için istek geldi.", id);
        return await _carService.GetCarDetailsAsync(id);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    public async Task<IActionResult> AddCar([FromBody] NewCarRequestDto request)
    {
        _logger.LogInformation("Yeni bir araba eklemek için istek geldi. {Request}", request);
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("You have to provide a user ID.");
        return Ok(await _carService.AddCarAsync(request, userId));
    }
    
    [AllowAnonymous]
    [HttpPost("price/change")]
    public async Task<IActionResult> PostPriceChange([FromBody] PriceChange priceChange)
    {
        try
        {
            await _elasticSearchService.IndexPriceChangeAsync(priceChange);
            return Ok("Price change indexed successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest("Error: " + ex.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("price/details/{id}")]
    public async Task<IActionResult> GetPriceChanges(int id)
    {
        try
        {
            var priceChanges = await _elasticSearchService.GetPriceChangesWithPercentagesAsync(id);
            return Ok(priceChanges);
        }
        catch (Exception ex)
        {
            return BadRequest("Error: " + ex.Message);
        }
    }
}