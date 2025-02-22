namespace CarSalesApplication.BLL.Interfaces;

public interface ICacheService
{
    Task<bool> SetAsync<T>(string key, T value, TimeSpan? duration = null);
    Task<T> GetAsync<T>(string key);
}
