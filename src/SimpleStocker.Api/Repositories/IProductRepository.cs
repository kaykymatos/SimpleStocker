using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetAllTasksByCategoryId(long categoryId);
    }
}
