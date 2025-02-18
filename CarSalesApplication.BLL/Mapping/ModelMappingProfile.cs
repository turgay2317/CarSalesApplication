using AutoMapper;
using CarSalesApplication.BLL.DTOs.Responses.Model;
using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.BLL.Mapping;

public class ModelMappingProfile : Profile
{
    public ModelMappingProfile()
    {
        CreateMap<Model, ModelDto>();
        CreateMap<Model, ModelDtoWithCars>()
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Cars, opt => opt.MapFrom(src => src.Cars));
    }
}