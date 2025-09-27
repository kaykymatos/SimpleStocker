using SimpleStocker.SaleApi.Models;
using SimpleStocker.Shared.Models.Interfaces.Repositories;

namespace SimpleStocker.SaleApi.Repositories
{
    public interface ISaleItemRepository : IBaseRepository<SaleItemModel>
    {
        Task<List<SaleItemModel>> MultipleUpdateAsync(List<SaleItemModel> models);
    }
}
