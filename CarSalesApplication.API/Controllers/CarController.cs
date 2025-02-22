using System.Security.Claims;
using CarSalesApplication.BLL.DTOs.Requests.Car;
using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.BLL.Interfaces;
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
    public CarController(
        ICarService carService, 
        IElasticSearchService elasticsearchService 
        ) {
        _carService = carService;
        _elasticSearchService = elasticsearchService;
    }
    
    //[Authorize(Policy = "User")]
    [AllowAnonymous]
    [HttpGet("")]
    public async Task<List<CarDto>> GetAllCars([FromQuery] PostStatus? type)
    { 
        return await _carService.GetAllCarsAsync(type);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<CarDtoWithDetails> GetCarDetails(int id)
    {
        return await _carService.GetCarDetailsAsync(id);
    }
    
    [AllowAnonymous]
    [HttpGet("search")]
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
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail)) return Unauthorized("You have to provide correct authorization.");
        return Ok(await _carService.AddCarAsync(request, userEmail));
    }
    
    /*
    [AllowAnonymous]
    [HttpGet("all/reset")]
    public async Task<IActionResult> SetAllCarsToElasticSearch([FromQuery] PostStatus? type)
    {
        var cars = await _carService.GetAllCarsAsync(type);
        await _elasticSearchService.AddOrUpdateBulk(cars, "cars");
        return Ok(cars);
    }
    */
}