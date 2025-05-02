using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;

namespace SimpleStocker.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public Task<ApiResponse<ProductViewModel>> CreateAsync(ProductViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ProductViewModel>> DeleteAsync(ProductViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetOneAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ProductViewModel>> UpdateAsync(ProductViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
