using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApplication.Presentation.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }
        
    [AllowAnonymous]
    [HttpGet]
    public async Task<List<BrandDto>> GetAllBrands()
    {
        return await _brandService.GetBrandsAsync();
    }
}