using SimpleStocker.SaleApi.Models;

namespace SimpleStocker.SaleApi.Repositories
{
    public interface ISaleRepository : IBaseRepository<SaleModel>
    {
        Task<bool> DeleteManyAsync(List<long> ids);
    }
}
