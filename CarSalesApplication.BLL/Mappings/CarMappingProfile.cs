using AutoMapper;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.BLL.DTOs.Responses.Car;

namespace CarSalesApplication.BLL.Mappings
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src._Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src._Model))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos));
            
            CreateMap<Car, CarDtoWithDetails>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src._Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src._Model))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src.CarParts));
        }
    }
}
