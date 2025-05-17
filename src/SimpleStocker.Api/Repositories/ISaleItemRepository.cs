using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public interface ISaleItemRepository : IBaseRepository<SaleItem>
    {

        Task<List<SaleItem>> GetAllSaleItemsBySaleId(long saleId);
        Task<bool> DeleteBySaleId(long saleId);
        Task<List<SaleItem>> GetAllSaleItemsByProductId(long productId);
    }
}
