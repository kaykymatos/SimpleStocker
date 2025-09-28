using SimpleStocker.InventoryApi.DTO;
using SimpleStocker.Shared.Models.Interfaces.Services;
using SimpleStocker.Shared.Models.Models;

namespace SimpleStocker.InventoryApi.Services
{
    public interface IInventoryService : IBaseService<InventoryDTO>
    {
        Task<ApiResponse<IList<InventoryDTO>>> GetStockByProductIdList(List<long> productIds);
        Task<ApiResponse<IList<InventoryDTO>>> UpdateStockAsync(List<InventoryDTO> model);
    }
}
