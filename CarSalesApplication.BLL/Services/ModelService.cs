using AutoMapper;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.DTOs.Responses.Model;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.DAL.Interfaces;

namespace CarSalesApplication.BLL.Services;

public class ModelService : IModelService
{
    private readonly IModelRepository _modelRepository;
    private readonly IMapper _mapper;
    
    public ModelService(IModelRepository modelRepository, IMapper mapper)
    {
        _modelRepository = modelRepository;
        _mapper = mapper;
    }
    
    public async Task<ModelDtoWithCars> GetModelByIdAsync(int id)
    {
        return _mapper.Map<ModelDtoWithCars>(await _modelRepository.GetByIdAsync(id));
    }
}