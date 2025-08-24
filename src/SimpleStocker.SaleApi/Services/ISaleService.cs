using SimpleStocker.SaleApi.DTO;

namespace SimpleStocker.SaleApi.Services
{
    public interface ISaleService : IBaseService<SaleDTO>
    {
        Task<ApiResponse<bool>> DeleteManyAsync(List<long> ids);
    }
}
