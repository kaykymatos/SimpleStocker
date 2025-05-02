using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public class ClientRepository : IClientRepository
    {
        public ClientRepository()
        {

        }

        public Task<Client> CreateAsync(Client entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Client entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Client>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetOneAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Client> UpdateAsync(Client entity)
        {
            throw new NotImplementedException();
        }
    }
}
