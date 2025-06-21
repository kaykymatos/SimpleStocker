using Microsoft.EntityFrameworkCore;
using SimpleStocker.ProductApi.Context;
using SimpleStocker.ProductApi.Models;

namespace SimpleStocker.ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiContext _context;
        public ProductRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<ProductModel> CreateAsync(ProductModel model)
        {
            _context.Products.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            _context.Products.Remove(model!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<ProductModel>> GetAllAsync()
        {
            try
            {
                var modelsList = await _context.Products.ToListAsync();
                return modelsList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ProductModel> GetOneAsync(long id)
        {
            var modelsList = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return modelsList;
        }

        public async Task<ProductModel> UpdateAsync(long id, ProductModel model)
        {
            _context.Products.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
