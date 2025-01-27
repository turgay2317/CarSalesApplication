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
    
    public async Task<List<Car>> GetCarsAsync()
    {
        return await _context.Cars.Include(c => c._Brand).ToListAsync();
    }

    public async Task<bool> AddCarAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        return await _context.SaveChangesAsync() > 0;
    }
}