using AutoMapper;
using CarSalesApplication.BLL.DTOs.Requests.Auth;
using CarSalesApplication.BLL.DTOs.Responses.User;
using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.BLL.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<SignUpRequestDto, User>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserStatus.User));
        }
    }
}