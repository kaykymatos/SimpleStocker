using SimpleStocker.ProductApi.DTO;

namespace SimpleStocker.ProductApi.Services
{
    public interface IProductService : IBaseService<ProductDTO>
    {
        Task<ApiResponse<bool>> DeleteManyAsync(List<long> ids);
    }
}
