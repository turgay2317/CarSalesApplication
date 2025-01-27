using AutoMapper;
using CarSalesApplication.BLL.DTOs.Requests;
using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.BLL.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.User));
        }
    }
}