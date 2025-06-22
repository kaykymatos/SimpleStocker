using Microsoft.EntityFrameworkCore;
using SimpleStocker.SaleApi.Context;
using SimpleStocker.SaleApi.Models.Entities;

namespace SimpleStocker.SaleApi.Repositories
{
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly ApiContext _context;

        public SaleItemRepository(ApiContext context)
        {
            _context = context;
        }
        public async Task<SaleItemModel> CreateAsync(SaleItemModel model)
        {
            _context.SaleItems.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await _context.SaleItems.FirstOrDefaultAsync(x => x.Id == id);
            _context.SaleItems.Remove(model!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<SaleItemModel>> GetAllAsync()
        {
            try
            {
                var modelsList = await _context.SaleItems.ToListAsync();
                return modelsList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<SaleItemModel> GetOneAsync(long id)
        {
            var modelsList = await _context.SaleItems.FirstOrDefaultAsync(x => x.Id == id);
            return modelsList;
        }

        public async Task<SaleItemModel> UpdateAsync(long id, SaleItemModel model)
        {
            _context.SaleItems.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
