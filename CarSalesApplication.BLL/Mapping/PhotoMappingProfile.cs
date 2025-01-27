using AutoMapper;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.BLL.DTOs.Shared;

namespace CarSalesApplication.BLL.Mapping
{
    public class PhotoMappingProfile : Profile
    {
        public PhotoMappingProfile()
        {
            CreateMap<Photo, PhotoDto>();
        }
    }
}
