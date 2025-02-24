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
    private readonly ISearchService _searchService;
    public CarController(
        ICarService carService, 
        ISearchService  searchService
        ) {
        _carService = carService;
        _searchService = searchService;
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
        return await _searchService.GetAll(keyword);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    public async Task<IActionResult> AddCar([FromBody] NewCarRequestDto request)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail)) return Unauthorized("You have to provide correct authorization.");
        return Ok(await _carService.AddCarAsync(request, userEmail));
    }
    
}