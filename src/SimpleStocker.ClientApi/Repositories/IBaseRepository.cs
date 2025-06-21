namespace SimpleStocker.ClientApi.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(long id, T model);
        Task<IList<T>> GetAllAsync();
        Task<T> GetOneAsync(long id);
        Task<bool> DeleteAsync(long id);
    }
}
