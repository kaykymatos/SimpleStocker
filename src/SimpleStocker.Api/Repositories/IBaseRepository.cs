using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetOneAsync();
    }
}
