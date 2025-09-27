using SimpleStocker.SaleApi.Models;
using SimpleStocker.Shared.Models.Interfaces.Repositories;

namespace SimpleStocker.SaleApi.Repositories
{
    public interface ISaleRepository : IBaseRepository<SaleModel>
    {
        Task<bool> DeleteManyAsync(List<long> ids);
    }
}
