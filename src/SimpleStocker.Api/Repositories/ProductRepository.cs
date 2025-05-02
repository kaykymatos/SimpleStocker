using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {

        }

        public Task<Product> CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetOneAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
