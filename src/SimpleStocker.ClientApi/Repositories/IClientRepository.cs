using SimpleStocker.ClientApi.Models;

namespace SimpleStocker.ClientApi.Repositories
{
    public interface IClientRepository : IBaseRepository<ClientModel>
    {
        Task<bool> DeleteManyAsync(List<long> ids);
    }
}
