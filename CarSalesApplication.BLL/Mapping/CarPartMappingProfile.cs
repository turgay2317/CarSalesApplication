using AutoMapper;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.BLL.DTOs.Shared;

namespace CarSalesApplication.BLL.Mapping
{
    public class CarPartMappingProfile : Profile
    {
        public CarPartMappingProfile()
        {
            CreateMap<CarPart, CarPartDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Part.Name));
        }
    }
}
