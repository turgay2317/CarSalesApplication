using System.Security.Claims;
using CarSalesApplication.BLL.DTOs.Requests.Car;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApplication.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{

    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<List<CarDto>> GetCars()
    {
        return await _carService.GetCarsAsync();
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    public async Task<IActionResult> AddCar([FromBody] NewCarRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized("You have to provide a user ID.");
        return Ok(await _carService.AddCarAsync(request, userId));
    }
}