using SimpleStocker.ProductApi.DTO;
using SimpleStocker.Shared.Models.Interfaces.Services;
using SimpleStocker.Shared.Models.Models;

namespace SimpleStocker.ProductApi.Services
{
    public interface IProductService : IBaseService<ProductDTO>
    {
        Task<ApiResponse<bool>> DeleteManyAsync(List<long> ids);
    }
}
