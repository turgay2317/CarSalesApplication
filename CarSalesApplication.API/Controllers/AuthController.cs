using CarSalesApplication.BLL.DTOs.Requests;
using CarSalesApplication.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApplication.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
    {
        return Ok(await _userService.GetUserToken(request));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        return Ok(await _userService.RegisterUser(request));
    }
}