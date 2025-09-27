using SimpleStocker.ProductApi.Models;
using SimpleStocker.Shared.Models.Interfaces.Repositories;

namespace SimpleStocker.ProductApi.Repositories
{
    public interface IProductRepository : IBaseRepository<ProductModel>
    {
        Task<bool> DeleteManyAsync(List<long> ids);
        Task<List<ProductModel>> MultipleUpdateAsync(List<ProductModel> models);
    }
}
