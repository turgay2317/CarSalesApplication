using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApplication.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelController : ControllerBase
{
    private readonly IModelService _modelService;
    
    public ModelController(IModelService modelService)
    {
        _modelService = modelService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ModelDto>> Get([FromRoute] int id)
    {
        return await _modelService.GetModelByIdAsync(id);
    }
}