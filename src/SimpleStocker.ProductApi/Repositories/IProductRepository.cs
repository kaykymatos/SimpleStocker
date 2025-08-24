using SimpleStocker.ProductApi.Models;

namespace SimpleStocker.ProductApi.Repositories
{
    public interface IProductRepository : IBaseRepository<ProductModel>
    {
        Task<bool> DeleteManyAsync(List<long> ids);
    }
}
