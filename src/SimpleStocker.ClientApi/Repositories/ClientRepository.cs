using Microsoft.EntityFrameworkCore;
using SimpleStocker.ClientApi.Context;
using SimpleStocker.ClientApi.Models;

namespace SimpleStocker.ClientApi.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApiContext _context;
        public ClientRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<ClientModel> CreateAsync(ClientModel model)
        {
            _context.Clients.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            _context.Clients.Remove(model!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<ClientModel>> GetAllAsync()
        {
            try
            {
                var modelsList = await _context.Clients.ToListAsync();
                return modelsList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClientModel> GetOneAsync(long id)
        {
            var modelsList = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            return modelsList;
        }

        public async Task<ClientModel> UpdateAsync(long id, ClientModel model)
        {
            _context.Clients.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
