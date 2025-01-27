using CarSalesApplication.BLL.DTOs.Requests;
using CarSalesApplication.BLL.DTOs.Responses;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;

namespace CarSalesApplication.BLL.Interfaces;

public interface IUserService
{
    public Task<User?> GetUserByEmailAndPassword(string email, string password);
    public Task<AuthResponseDto?> GetUserToken(AuthRequestDto authRequestDto);
    public Task<AuthResponseDto> RegisterUser(RegisterRequestDto registerRequestDto);
}