using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Api.Services
{
    public interface IBaseService<T> where T : BaseViewModel
    {
        Task<ApiResponse<T>> CreateAsync(T entity);
        Task<ApiResponse<T>> UpdateAsync(T entity);
        Task<ApiResponse<T>> DeleteAsync(long id);
        Task<ApiResponse<List<T>>> GetAllAsync();
        Task<ApiResponse<T>> GetOneAsync(long id);
        Task ClearDb();
    }
}
