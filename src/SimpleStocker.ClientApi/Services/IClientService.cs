using SimpleStocker.ClientApi.DTO;

namespace SimpleStocker.ClientApi.Services
{
    public interface IClientService : IBaseService<ClientDTO>
    {
        Task<ApiResponse<bool>> DeleteManyAsync(List<long> ids);
    }
}
