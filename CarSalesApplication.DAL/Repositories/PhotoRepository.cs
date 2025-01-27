using CarSalesApplication.DAL.Entities;
using CarSalesApplication.DAL.Interfaces;

namespace CarSalesApplication.DAL.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly AppDbContext _context;

    public PhotoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> SavePhotosAsync(IEnumerable<Photo> photos)
    {
        await _context.Photos.AddRangeAsync(photos);
        return await _context.SaveChangesAsync() > 0;
    }
}