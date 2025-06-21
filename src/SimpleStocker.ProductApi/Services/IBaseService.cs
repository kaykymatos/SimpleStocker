using SimpleStocker.ProductApi.DTO;

namespace SimpleStocker.ProductApi.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<ApiResponse<T>> CreateAsync(T model);
        Task<ApiResponse<T>> UpdateAsync(long id, T model);
        Task<ApiResponse<IList<T>>> GetAllAsync();
        Task<ApiResponse<T>> GetOneAsync(long id);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
