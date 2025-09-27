using SimpleStocker.InventoryApi.Models;
using SimpleStocker.Shared.Models.Interfaces.Repositories;

namespace SimpleStocker.InventoryApi.Repositories
{
    public interface IInventoryRepository : IBaseRepository<InventoryModel>
    {
        Task<List<InventoryModel>> GetStockByProductIdList(List<long> productIds);
        Task<List<InventoryModel>> UpdateStockAsync(List<InventoryModel> updateValues);
    }
}
