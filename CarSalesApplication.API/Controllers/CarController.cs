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
    public CarController(ICarService carService, ElasticSearchService elasticsearchService, ILogger<CarController> logger)
    {
        _carService = carService;
        _elasticSearchService = elasticsearchService;
        _logger = logger;
    }
    
    //[Authorize(Roles = "Admin")]
    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<List<CarDto>> GetAllCars([FromQuery] PostType? type)
    { 
        _logger.LogInformation("{PostType} tipi arabalar için istek geldi", type);
        return await _carService.GetAllCarsAsync(type);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<CarDtoWithProfile> GetCarDetails(int id)
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