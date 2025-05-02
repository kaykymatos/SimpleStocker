using SimpleStocker.Api.Models.Entities;
using SimpleStocker.Api.Models.ViewModels;
using System.Collections.Generic;

namespace SimpleStocker.Api.Services
{
    public interface IBaseService<T> where T : T
    {
        Task<ApiResponse<T>> CreateAsync(T entity);
        Task<ApiResponse<T>> UpdateAsync(T entity);
        Task<ApiResponse<T>> DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetOneAsync();
    }
}
