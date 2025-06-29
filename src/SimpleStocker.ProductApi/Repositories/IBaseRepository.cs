namespace SimpleStocker.ProductApi.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(T model);
        Task<IList<T>> GetAllAsync();
        Task<T> GetOneAsync(long id);
        Task<bool> DeleteAsync(long id);
        Task<List<T>> MultipleUpdateAsync(List<T> models);
    }
}
