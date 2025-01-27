using AutoMapper;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.BLL.DTOs.Responses;

namespace CarSalesApplication.BLL.Mapping
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<Brand, BrandDto>();
        }
    }
}
