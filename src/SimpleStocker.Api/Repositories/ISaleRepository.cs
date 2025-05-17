using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        Task<List<Sale>> GetAllSalesByClientId(long clientId);
    }
}
