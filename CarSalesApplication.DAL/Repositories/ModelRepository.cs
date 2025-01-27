using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.DAL.Repositories;

public class ModelRepository : IModelRepository
{
    private readonly AppDbContext _context;

    public ModelRepository(AppDbContext context)
    {
        _context = context;
    }
    
    // Modele göre arabaları ve fotoğraflarını döndürür.
    public async Task<Model?> GetByIdAsync(int id)
    {
        return await _context.Models
            .Include(m => m.Cars)
            .ThenInclude(c => c.Photos)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}