using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.DAL.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly AppDbContext _context;

    public BrandRepository(AppDbContext context)
    {
        this._context = context;
    }
    
    public async Task<List<Brand>> GetBrandsAsync()
    { 
        return await _context.Brands.ToListAsync();
    }

    public async Task<Brand?> GetBrandByIdAsync(int brandId)
    {
        return await _context.Brands.Include(b => b.Models).FirstOrDefaultAsync(b => b.Id == brandId);
    }
}