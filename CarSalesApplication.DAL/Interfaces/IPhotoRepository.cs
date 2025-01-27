using CarSalesApplication.DAL.Entities;

namespace CarSalesApplication.DAL.Interfaces;

public interface IPhotoRepository
{
    public Task<bool> SavePhotosAsync(IEnumerable<Photo> photos);
}