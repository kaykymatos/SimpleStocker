using SimpleStocker.ProductApi.DTO;

namespace SimpleStocker.ProductApi.Services
{
    public interface ICategoryService : IBaseService<CategoryDTO>
    {
        Task<ApiResponse<bool>> DeleteManyAsync(List<long> ids);
    }
}
