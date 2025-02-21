using AutoMapper;
using CarSalesApplication.BLL.DTOs.Requests.Auth;
using CarSalesApplication.BLL.DTOs.Responses.Auth;
using CarSalesApplication.BLL.Interfaces;
using CarSalesApplication.Core.Helper;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarSalesApplication.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly KeycloakHelper _keycloakHelper;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
    
    public UserService(IUserRepository userRepository, KeycloakHelper keycloakHelper, IMapper mapper, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _keycloakHelper = keycloakHelper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        return await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
    }

    public async Task<AuthResponseDto?> GetUserToken(SignInRequestDto signInRequestDto)
    {
        _logger.LogInformation("Giriş yapma isteği geldi. {Email}", signInRequestDto.Email);
        User? authUser = await _userRepository.GetUserByEmailAndPasswordAsync(signInRequestDto.Email, signInRequestDto.Password);
        AuthResponseDto authResponseDto = new AuthResponseDto();
        if (authUser != null)
        {
            try
            {
                authResponseDto.Token = await _keycloakHelper.GetAccessTokenAsync(authUser.Email, authUser.Password);
                _logger.LogInformation("Kullanıcıya JWT Token atandı. {Token}", authResponseDto.Token);
            }
            catch (Exception e)
            {
                _logger.LogError("Keycloak tokenda bir hata oluştu. {Error}", e.Message);
            }
        }
        else
        {
            _logger.LogError("Sistemde kayıtlı kullanıcı bulunamadı. {Email}", signInRequestDto.Email);
        }
        return authResponseDto;
    }

    public async Task<AuthResponseDto> RegisterUser(SignUpRequestDto signUpRequestDto)
    {
        _logger.LogInformation("Kayıt olma isteği geldi: {Name} {Surname} {Email}", signUpRequestDto.Name, signUpRequestDto.Surname, signUpRequestDto.Email);
        try
        {
            User newUser = _mapper.Map<User>(signUpRequestDto);
            
            // Keycloak kayıt olmayı dene
            var authResponse = await _keycloakHelper.CreateUserAsync(newUser.Email, newUser.Name,newUser.Surname, newUser.Password);
            
            if (authResponse == false)
            {
                _logger.LogError("Keycloak kaydı başarısız oldu. {Name} {Surname} {Email}", signUpRequestDto.Name, signUpRequestDto.Surname, signUpRequestDto.Email);
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
            
            _logger.LogError("AccessToken alınırken hata oluştu. {Name} {Surname} {Email}", signUpRequestDto.Name, signUpRequestDto.Surname, signUpRequestDto.Email);
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Kullanıcı aynı e-postayla kaydolmaya çalıştı. {Name} {Surname} {Email}", signUpRequestDto.Name, signUpRequestDto.Surname, signUpRequestDto.Email);
            throw new Exception("Failed to register same email.");
        }

        return await Task.FromException<AuthResponseDto>(new Exception("Kayıt başarısız."));
    }
}