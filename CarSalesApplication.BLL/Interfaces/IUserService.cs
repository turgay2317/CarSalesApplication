using CarSalesApplication.BLL.DTOs.Requests.Auth;
using CarSalesApplication.BLL.DTOs.Responses.Auth;
using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.BLL.Interfaces;

public interface IUserService
{
    public Task<User?> GetUserByEmailAndPassword(string email, string password);
    public Task<AuthResponseDto?> GetUserToken(SignInRequestDto signInRequestDto);
    public Task<AuthResponseDto> RegisterUser(SignUpRequestDto signUpRequestDto);
}