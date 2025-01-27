using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}