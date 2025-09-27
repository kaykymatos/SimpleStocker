using SimpleStocker.ClientApi.DTO;
using SimpleStocker.Shared.Models.Interfaces.Services;
using SimpleStocker.Shared.Models.Models;

namespace SimpleStocker.ClientApi.Services
{
    public interface IClientService : IBaseService<ClientDTO>
    {
        Task<ApiResponse<bool>> DeleteManyAsync(List<long> ids);
    }
}
