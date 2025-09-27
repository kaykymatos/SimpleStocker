using SimpleStocker.InventoryApi.DTO;
using SimpleStocker.Shared.Models.Interfaces.Services;
using SimpleStocker.Shared.Models.Models;

namespace SimpleStocker.InventoryApi.Services
{
    public interface IInventoryService : IBaseService<InventoryDTO>
    {
        Task<ApiResponse<List<InventoryDTO>>> GetStockByProductIdList(List<long> productIds);
        Task<ApiResponse<List<InventoryDTO>>> UpdateStockAsync(List<InventoryDTO> model);
    }
}
