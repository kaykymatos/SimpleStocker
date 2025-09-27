using SimpleStocker.ProductApi.Models;
using SimpleStocker.Shared.Models.Interfaces.Repositories;

namespace SimpleStocker.ProductApi.Repositories
{
    public interface ICategoryRepository : IBaseRepository<CategoryModel>
    {
        Task<bool> DeleteManyAsync(List<long> ids);
    }
}
