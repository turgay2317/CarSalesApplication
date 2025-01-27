using AutoMapper;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.BLL.DTOs.Responses;

namespace CarSalesApplication.BLL.Mapping
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarDto>();
        }
    }
}
