using SimpleStocker.ProductApi.Models;

namespace SimpleStocker.ProductApi.Repositories
{
    public interface ICategoryRepository : IBaseRepository<CategoryModel>
    {
        Task<bool> DeleteManyAsync(List<long> ids);
    }
}
