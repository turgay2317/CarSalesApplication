using AutoMapper;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.BLL.Mapping;

public class ModelMappingProfile : Profile
{
    public ModelMappingProfile()
    {
        CreateMap<Model, ModelDto>();
    }
}