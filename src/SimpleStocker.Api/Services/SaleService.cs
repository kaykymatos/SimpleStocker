using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;

namespace SimpleStocker.Api.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        public SaleService(ISaleRepository repository)
        {
            _repository = repository;
        }
        public Task<ApiResponse<SaleViewModel>> CreateAsync(SaleViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<SaleViewModel>> DeleteAsync(SaleViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<SaleViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SaleViewModel> GetOneAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<SaleViewModel>> UpdateAsync(SaleViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
