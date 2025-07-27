using Microsoft.EntityFrameworkCore;
using SimpleStocker.SaleApi.Context;
using SimpleStocker.SaleApi.Models;

namespace SimpleStocker.SaleApi.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApiContext _context;
        public SaleRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<SaleModel> CreateAsync(SaleModel model)
        {
            try
            {
                _context.Sales.Add(model);
                await _context.SaveChangesAsync(); // Isso gera o ID da venda

                model.Items.ForEach(item =>
                {
                    item.Id = 0; // evita erro de IDENTITY
                    item.SaleId = model.Id;
                });

                _context.SaleItems.AddRange(model.Items);
                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await _context.Sales.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
            _context.SaleItems.RemoveRange(model!.Items);
            _context.Sales.Remove(model!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<SaleModel>> GetAllAsync()
        {
            try
            {
                var modelsList = await _context.Sales.ToListAsync();
                return modelsList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<SaleModel> GetOneAsync(long id)
        {
            var modelsList = await _context.Sales.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
            return modelsList;
        }

        public async Task<SaleModel> UpdateAsync(long id, SaleModel model)
        {
            _context.Sales.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
