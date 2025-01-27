using AutoMapper;
using CarSalesApplication.BLL.DTOs.Requests.Auth;
using CarSalesApplication.BLL.DTOs.Responses.Auth;
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

    public async Task<AuthResponseDto?> GetUserToken(SignInRequestDto signInRequestDto)
    {
        User? authUser = await _userRepository.GetUserByEmailAndPasswordAsync(signInRequestDto.Email, signInRequestDto.Password);
        AuthResponseDto authResponseDto = new AuthResponseDto();
        authResponseDto.Token = authUser != null ? _jwtHelper.GenerateToken(authUser.Id,authUser.UserType.ToString()) : null;
        return authResponseDto;
    }

    public async Task<AuthResponseDto> RegisterUser(SignUpRequestDto signUpRequestDto)
    {
        try
        {
            User newUser = _mapper.Map<User>(signUpRequestDto);
            await _userRepository.AddUserAsync(newUser);

            AuthResponseDto? authResponse = await GetUserToken(new SignInRequestDto()
            {
                Email = signUpRequestDto.Email,
                Password = signUpRequestDto.Password
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