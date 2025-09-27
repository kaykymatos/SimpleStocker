using Mapster;
using SimpleStocker.Caching;
using SimpleStocker.Caching.Services;
using SimpleStocker.ClientApi.DTO;
using SimpleStocker.ClientApi.Models;
using SimpleStocker.ClientApi.Repositories;
using SimpleStocker.ClientApi.Util;
using SimpleStocker.ClientApi.Validations;
using SimpleStocker.Shared.Models.Models;
using System.Text.Json;

namespace SimpleStocker.ClientApi.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly ICachingService _cache;
        public ClientService(IClientRepository repository, ICachingService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<ApiResponse<ClientDTO>> CreateAsync(ClientDTO model)
        {
            var validation = new ClientValidator().Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<ClientDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));
            try
            {
                var res = await _repository.CreateAsync(model.Adapt<ClientModel>());
                if (res == null)
                    return new ApiResponse<ClientDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<ClientDTO>(true, "", [], res.Adapt<ClientDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            try
            {
                var foundEntity = await _repository.GetOneAsync(id);
                if (foundEntity == null)
                    return new ApiResponse<bool>("Id", "Id não encontrado!");

                var deleteItem = await _repository.DeleteAsync(id);
                if (deleteItem)
                {
                    await _cache.RemoveAsync(string.Format(CacheKeys.GetOneClient, id));
                    return new ApiResponse<bool>(true, "", [], true, 200);
                }
                return new ApiResponse<bool>("Server", "Erro ao deletar item");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteManyAsync(List<long> ids)
        {
            try
            {
                var result = await _repository.DeleteManyAsync(ids);
                if (result)
                {
                    await _cache.RemoveListAsync(ids.Select(id => string.Format(CacheKeys.GetOneClient, id)).ToList());
                    return new ApiResponse<bool>(true, "", [], true, 200);
                }
                return new ApiResponse<bool>("Server", "Erro ao deletar itens");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<IList<ClientDTO>>> GetAllAsync()
        {
            try
            {
                var cachedCategories = await _cache.GetAsync(CacheKeys.GetAllClients);
                if (!string.IsNullOrEmpty(cachedCategories))
                {
                    var cachedResponse = new ApiResponse<IList<ClientDTO>>(JsonSerializer.Deserialize<IList<ClientDTO>>(cachedCategories));
                    return cachedResponse;
                }
                var dbReturn = await _repository.GetAllAsync();
                var response = new ApiResponse<IList<ClientDTO>>(dbReturn.Adapt<IList<ClientDTO>>());
                await _cache.SetAsync(CacheKeys.GetAllClients, JsonSerializer.Serialize(response.Data));
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ClientDTO>> GetOneAsync(long id)
        {
            try
            {
                var cachedClient = await _cache.GetAsync(string.Format(CacheKeys.GetOneClient, id));
                if (!string.IsNullOrEmpty(cachedClient))
                {
                    var cachedResponse = new ApiResponse<ClientDTO>(JsonSerializer.Deserialize<ClientDTO>(cachedClient));
                    return cachedResponse;
                }
                var dbReturn = await _repository.GetOneAsync(id);
                var response = new ApiResponse<ClientDTO>(dbReturn.Adapt<ClientDTO>());

                await _cache.SetAsync(string.Format(CacheKeys.GetOneClient, id), JsonSerializer.Serialize(response.Data));
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ClientDTO>> UpdateAsync(long id, ClientDTO model)
        {
            var originalmodel = await _repository.GetOneAsync(id);
            if (originalmodel == null)
                return new ApiResponse<ClientDTO>("Id", "Item não encontrado");

            var validation = new ClientValidator(true).Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<ClientDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                var clients = JsonSerializer.Deserialize<List<ClientDTO>>(await _cache.GetAsync(CacheKeys.GetAllClients));
                model.Adapt(originalmodel);

                var clientUpdate = clients.FirstOrDefault(x => x.Id == id);
                model.Adapt(clientUpdate);
                clientUpdate.UpdatedDate = DateTime.UtcNow;

                var updateRepoTask = _repository.UpdateAsync(originalmodel);
                var updateCacheTask = _cache.SetAsync(
                    CacheKeys.GetAllClients,
                    JsonSerializer.Serialize(clients.Adapt<List<ClientDTO>>())
                );
                var updateProductCacheTask = _cache.SetAsync(
                    string.Format(CacheKeys.GetOneClient, id),
                    JsonSerializer.Serialize(clientUpdate)
                );

                await Task.WhenAll(updateRepoTask, updateCacheTask, updateProductCacheTask);

                var updatedEntity = await updateRepoTask;

                return new ApiResponse<ClientDTO>(true, "", [], updatedEntity.Adapt<ClientDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
