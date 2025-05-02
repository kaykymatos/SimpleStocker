using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;

namespace SimpleStocker.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository repository;
        public ClientService(IClientRepository repository)
        {
            this.repository = repository;
        }
        public Task<ApiResponse<ClientViewModel>> CreateAsync(ClientViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ClientViewModel>> DeleteAsync(ClientViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ClientViewModel> GetOneAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ClientViewModel>> UpdateAsync(ClientViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
