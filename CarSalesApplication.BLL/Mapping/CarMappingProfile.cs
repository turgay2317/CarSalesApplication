using AutoMapper;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.DTOs.Responses.Car;

namespace CarSalesApplication.BLL.Mapping
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<Car, CarDtoWithProfile>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
