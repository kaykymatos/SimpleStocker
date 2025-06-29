using SimpleStocker.InventoryApi.Models;

namespace SimpleStocker.InventoryApi.Repositories
{
    public interface IInventoryRepository : IBaseRepository<InventoryModel>
    {

        Task<List<InventoryModel>> UpdateStockAsync(List<InventoryModel> updateValues);
    }
}
