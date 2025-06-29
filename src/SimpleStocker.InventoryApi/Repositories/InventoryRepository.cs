using Microsoft.EntityFrameworkCore;
using SimpleStocker.InventoryApi.Context;
using SimpleStocker.InventoryApi.Models;

namespace SimpleStocker.InventoryApi.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApiContext _context;
        public InventoryRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<InventoryModel> CreateAsync(InventoryModel model)
        {
            _context.InventoryModel.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await _context.InventoryModel.FirstOrDefaultAsync(x => x.Id == id);
            _context.InventoryModel.Remove(model!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<InventoryModel>> GetAllAsync()
        {
            try
            {
                var modelsList = await _context.InventoryModel.ToListAsync();
                return modelsList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<InventoryModel> GetOneAsync(long id)
        {
            var modelsList = await _context.InventoryModel.FirstOrDefaultAsync(x => x.Id == id);
            return modelsList;
        }

        public async Task<InventoryModel> UpdateAsync(long id, InventoryModel model)
        {
            _context.InventoryModel.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<InventoryModel>> UpdateStockAsync(List<InventoryModel> updateValues)
        {
            var models = await _context.InventoryModel.Where(x => updateValues.Select(x => x.ProductId).Contains(x.ProductId)).ToListAsync();

            foreach (var item in models)
                item.Quantity -= updateValues.Find(x => x.ProductId == item.ProductId).Quantity;


            _context.InventoryModel.UpdateRange(models);
            await _context.SaveChangesAsync();
            return models;
        }
    }
}
