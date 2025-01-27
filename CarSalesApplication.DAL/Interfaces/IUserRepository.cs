using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.DAL.Interfaces;

public interface IUserRepository
{
    public Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
    public Task AddUserAsync(User user);
}