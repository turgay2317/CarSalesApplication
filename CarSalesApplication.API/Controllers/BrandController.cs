using CarSalesApplication.BLL.DTOs.Responses.Brand;
using CarSalesApplication.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApplication.Presentation.Controllers;

[Authorize]
[Route("api/brand")]
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
    public async Task<List<BrandDto>> GetBrands()
    {
        return await _brandService.GetBrandsAsync();
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<BrandDtoWithModels>> GetBrand(int id)
    {
        return await _brandService.GetBrandByIdAsync(id);
    }
}