using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.DAL.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly AppDbContext _context;

    public BrandRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Brand> GetBrands()
    { 
        return _context.Brands.ToList();
    }

    public Brand? GetBrandById(int brandId)
    {
        return _context.Brands.Include(b => b.Models).FirstOrDefault(b => b.Id == brandId);
    }
}