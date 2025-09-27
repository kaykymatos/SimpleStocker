using SimpleStocker.ProductApi.DTO;
using SimpleStocker.Shared.Models.Interfaces.Services;
using SimpleStocker.Shared.Models.Models;

namespace SimpleStocker.ProductApi.Services
{
    public interface ICategoryService : IBaseService<CategoryDTO>
    {
        Task<ApiResponse<bool>> DeleteManyAsync(List<long> ids);
    }
}
