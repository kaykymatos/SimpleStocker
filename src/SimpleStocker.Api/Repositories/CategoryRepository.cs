using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository()
        {

        }

        public Task<Category> CreateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetOneAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
