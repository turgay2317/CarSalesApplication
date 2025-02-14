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
    private readonly KeycloakHelper _keycloakHelper;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, KeycloakHelper keycloakHelper, IMapper mapper)
    {
        _userRepository = userRepository;
        _keycloakHelper = keycloakHelper;
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
        authResponseDto.Token = authUser != null ? await _keycloakHelper.GetAccessTokenAsync(authUser.Email,authUser.Password) : null;
        return authResponseDto;
    }

    public async Task<AuthResponseDto> RegisterUser(SignUpRequestDto signUpRequestDto)
    {
        try
        {
            User newUser = _mapper.Map<User>(signUpRequestDto);
            
            // Keycloak kayıt olmayı dene
            var authResponse = await _keycloakHelper.CreateUserAsync(newUser.Email, newUser.Name,newUser.Surname, newUser.Password);
            
            if (authResponse == false)
            {
                throw new Exception("Failed to generate token.");
            }
            
            // Keycloak giriş yapmayı dene
            var accessToken = await _keycloakHelper.GetAccessTokenAsync(newUser.Email,newUser.Password);

            if (accessToken != null)
            {
                await _userRepository.AddUserAsync(newUser);
                return new AuthResponseDto()
                {
                    Token = accessToken,
                };
            }

            return null;
        }
        catch (DbUpdateException e)
        {
            throw new Exception("Failed to register same email.");
        }
    }
}