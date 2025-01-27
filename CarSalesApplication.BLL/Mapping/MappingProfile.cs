using AutoMapper;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.BLL.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Brand, BrandWithModelsDto>()
            .ForMember(dest => dest.Models, opt => opt.MapFrom(src => src.Models));
    }
}