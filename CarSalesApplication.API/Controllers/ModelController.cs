using CarSalesApplication.BLL.DTOs.Responses.Model;
using CarSalesApplication.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApplication.Presentation.Controllers;

[Route("api/model")]
[ApiController]
public class ModelController : ControllerBase
{
    private readonly IModelService _modelService;
    
    public ModelController(IModelService modelService)
    {
        _modelService = modelService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ModelDtoWithCars>> Get([FromRoute] int id)
    {
        return await _modelService.GetModelByIdAsync(id);
    }
}