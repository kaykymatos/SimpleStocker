using SimpleStocker.SaleApi.DTO;
using SimpleStocker.Shared.Models.Interfaces.Services;
using SimpleStocker.Shared.Models.Models;

namespace SimpleStocker.SaleApi.Services
{
    public interface ISaleService : IBaseService<SaleDTO>
    {
        Task<ApiResponse<bool>> DeleteManyAsync(List<long> ids);
    }
}
