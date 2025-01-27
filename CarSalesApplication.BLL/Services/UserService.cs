using AutoMapper;
using CarSalesApplication.BLL.DTOs.Requests;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.Core.Helper;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtHelper _jwtHelper;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, JwtHelper jwtHelper, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtHelper = jwtHelper;
        _mapper = mapper;
    }

    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        return await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
    }

    public async Task<AuthResponseDto?> GetUserToken(AuthRequestDto authRequestDto)
    {
        User? authUser = await _userRepository.GetUserByEmailAndPasswordAsync(authRequestDto.Email, authRequestDto.Password);
        AuthResponseDto authResponseDto = new AuthResponseDto();
        authResponseDto.Token = authUser != null ? _jwtHelper.GenerateToken(authUser.Id,authUser.UserType.ToString()) : null;
        return authResponseDto;
    }

    public async Task<AuthResponseDto> RegisterUser(RegisterRequestDto registerRequestDto)
    {
        try
        {
            User newUser = _mapper.Map<User>(registerRequestDto);
            await _userRepository.AddUserAsync(newUser);

            AuthResponseDto? authResponse = await GetUserToken(new AuthRequestDto()
            {
                Email = registerRequestDto.Email,
                Password = registerRequestDto.Password
            });

            if (authResponse == null)
            {
                throw new Exception("Failed to generate token.");
            }

            return authResponse;
        }
        catch (DbUpdateException e)
        {
            throw new Exception("Failed to register same email.");
        }
    }
}