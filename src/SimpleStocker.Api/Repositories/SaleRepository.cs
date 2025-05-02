using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        public SaleRepository()
        {

        }

        public Task<Sale> CreateAsync(Sale entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Sale entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Sale>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Sale> GetOneAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Sale> UpdateAsync(Sale entity)
        {
            throw new NotImplementedException();
        }
    }
}
