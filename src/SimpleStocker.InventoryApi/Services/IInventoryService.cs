using SimpleStocker.InventoryApi.DTO;

namespace SimpleStocker.InventoryApi.Services
{
    public interface IInventoryService : IBaseService<InventoryDTO>
    {
        Task<ApiResponse<List<InventoryDTO>>> GetStockByProductIdList(List<long> productIds);
        Task<ApiResponse<List<InventoryDTO>>> UpdateStockAsync(List<InventoryDTO> model);
    }
}
