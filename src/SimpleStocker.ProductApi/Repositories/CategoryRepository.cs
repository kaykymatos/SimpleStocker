using Microsoft.EntityFrameworkCore;
using SimpleStocker.ProductApi.Context;
using SimpleStocker.ProductApi.Models;

namespace SimpleStocker.ProductApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApiContext _context;

        public CategoryRepository(ApiContext context)
        {
            _context = context;
        }
        public async Task<CategoryModel> CreateAsync(CategoryModel model)
        {
            _context.Categories.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            _context.Categories.Remove(model!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<CategoryModel>> GetAllAsync()
        {
            try
            {
                var modelsList = await _context.Categories.ToListAsync();
                return modelsList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CategoryModel> GetOneAsync(long id)
        {
            var modelsList = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return modelsList;
        }

        public async Task<CategoryModel> UpdateAsync(CategoryModel model)
        {
            _context.Categories.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<CategoryModel>> MultipleUpdateAsync(List<CategoryModel> models)
        {
            _context.Categories.UpdateRange(models);
            await _context.SaveChangesAsync();
            return models;
        }
    }
}
