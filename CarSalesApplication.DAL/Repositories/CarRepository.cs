using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.DAL.Repositories;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<List<Car>> GetAllCarsAsync(PostStatus? type)
    {
        return await _context.Cars
            .Where(c =>  type == null || c.Status.Equals(type)) // Null = Any type indicated
            .Include(c => c._Brand)
            .Include(c => c._Model)
            .Include(c => c.Photos)
            .ToListAsync();
    }

    public async Task<Car?> GetCarByIdAsync(int carId)
    {
        return await _context.Cars
            .Where(c => c.Id == carId)
            .Include(c => c._Brand)
            .Include(c => c._Model)
            .Include(c => c.User)
            .Include(c => c.Photos)
            .Include(c => c.CarParts)
            .ThenInclude(cp => cp.Part)
            .FirstOrDefaultAsync();
    }
    
    public async Task<bool> AddCarAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        return await _context.SaveChangesAsync() > 0;
    }
}