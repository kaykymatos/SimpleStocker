namespace SimpleStocker.ProductApi.Caching.Services
{
    public interface ICachingService
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync(string key);
        Task RemoveAsync(string key);
        Task RemoveListAsync(List<string> keys);
    }
}
