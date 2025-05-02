using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;

namespace SimpleStocker.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public Task<ApiResponse<CategoryViewModel>> CreateAsync(CategoryViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CategoryViewModel>> DeleteAsync(CategoryViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryViewModel> GetOneAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CategoryViewModel>> UpdateAsync(CategoryViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
