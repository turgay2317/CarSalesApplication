using AutoMapper;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.BLL.DTOs.Responses.Brand;

namespace CarSalesApplication.BLL.Mappings
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<Brand, BrandDto>();
            
            CreateMap<Brand, BrandDtoWithModels>()
                .ForMember(dest => dest.Models, opt => opt.MapFrom(src => src.Models));
        }
    }
}
