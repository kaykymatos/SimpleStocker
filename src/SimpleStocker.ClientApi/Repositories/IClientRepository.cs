using SimpleStocker.ClientApi.Models;
using SimpleStocker.Shared.Models.Interfaces.Repositories;

namespace SimpleStocker.ClientApi.Repositories
{
    public interface IClientRepository : IBaseRepository<ClientModel>
    {
        Task<bool> DeleteManyAsync(List<long> ids);
    }
}
