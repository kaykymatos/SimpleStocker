using Mapster;
using SimpleStocker.Caching;
using SimpleStocker.Caching.Services;
using SimpleStocker.InventoryApi.DTO;
using SimpleStocker.InventoryApi.Models;
using SimpleStocker.InventoryApi.Repositories;
using SimpleStocker.InventoryApi.Util;
using SimpleStocker.InventoryApi.Validations;
using SimpleStocker.Shared.Models.Models;
using System.Text.Json;

namespace SimpleStocker.InventoryApi.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly ICachingService _cache;
        public InventoryService(IInventoryRepository repository, ICachingService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<ApiResponse<InventoryDTO>> CreateAsync(InventoryDTO model)
        {
            var validation = new InventoryValidator().Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<InventoryDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));
            try
            {
                var res = await _repository.CreateAsync(model.Adapt<InventoryModel>());
                if (res == null)
                    return new ApiResponse<InventoryDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<InventoryDTO>(true, "", [], res.Adapt<InventoryDTO>(), 200);
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
                    await _cache.RemoveAsync(string.Format(CacheKeys.GetOneInventory, id));
                    return new ApiResponse<bool>(true, "", [], true, 200);
                }
                return new ApiResponse<bool>("Server", "Erro ao deletar item");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<IList<InventoryDTO>>> GetAllAsync()
        {
            try
            {
                var cachedInventories = await _cache.GetAsync(CacheKeys.GetAllInventories);
                if (!string.IsNullOrEmpty(cachedInventories))
                {
                    var cachedResponse = new ApiResponse<IList<InventoryDTO>>(JsonSerializer.Deserialize<IList<InventoryDTO>>(cachedInventories));
                    return cachedResponse;
                }
                var dbReturn = await _repository.GetAllAsync();
                var response = new ApiResponse<IList<InventoryDTO>>(dbReturn.Adapt<IList<InventoryDTO>>());
                await _cache.SetAsync(CacheKeys.GetAllInventories, JsonSerializer.Serialize(response.Data));
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<InventoryDTO>> GetOneAsync(long id)
        {
            try
            {
                var cachedInventory = await _cache.GetAsync(string.Format(CacheKeys.GetOneInventory, id));
                if (!string.IsNullOrEmpty(cachedInventory))
                {
                    var cachedResponse = new ApiResponse<InventoryDTO>(JsonSerializer.Deserialize<InventoryDTO>(cachedInventory));
                    return cachedResponse;
                }
                var dbReturn = await _repository.GetOneAsync(id);
                var response = new ApiResponse<InventoryDTO>(dbReturn.Adapt<InventoryDTO>());

                await _cache.SetAsync(string.Format(CacheKeys.GetOneInventory, id), JsonSerializer.Serialize(response.Data));
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<IList<InventoryDTO>>> GetStockByProductIdList(List<long> productIds)
        {
            try
            {
                var cachedInventories = await _cache.GetAsync(CacheKeys.GetAllProductInventories);
                if (!string.IsNullOrEmpty(cachedInventories))
                {
                    var cachedResponse = new ApiResponse<IList<InventoryDTO>>(JsonSerializer.Deserialize<IList<InventoryDTO>>(cachedInventories));
                    return cachedResponse;
                }
                var dbReturn = await _repository.GetStockByProductIdList(productIds);
                var response = new ApiResponse<IList<InventoryDTO>>(dbReturn.Adapt<IList<InventoryDTO>>());
                if (response == null)
                    return new ApiResponse<IList<InventoryDTO>>("Server", "Erro ao tentar criar registro!");
                await _cache.SetAsync(CacheKeys.GetAllProductInventories, JsonSerializer.Serialize(response.Data));
                return response;
              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<InventoryDTO>> UpdateAsync(long id, InventoryDTO model)
        {
            var originalmodel = await _repository.GetOneAsync(id);
            if (originalmodel == null)
                return new ApiResponse<InventoryDTO>("Id", "Item não encontrado");

            var validation = new InventoryValidator(true).Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<InventoryDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                var inventories = JsonSerializer.Deserialize<List<InventoryDTO>>(await _cache.GetAsync(CacheKeys.GetAllInventories));
                model.Adapt(originalmodel);

                var inventoryUpdate = inventories.FirstOrDefault(x => x.Id == id);
                model.Adapt(inventoryUpdate);

                var updateRepoTask = _repository.UpdateAsync(originalmodel);
                var updateCacheTask = _cache.SetAsync(
                    CacheKeys.GetAllInventories,
                    JsonSerializer.Serialize(inventories.Adapt<List<InventoryDTO>>())
                );
                var updateInventoryCacheTask = _cache.SetAsync(
                    string.Format(CacheKeys.GetOneInventory, id),
                    JsonSerializer.Serialize(inventoryUpdate)
                );

                await Task.WhenAll(updateRepoTask, updateCacheTask, updateInventoryCacheTask);

                var updatedEntity = await updateRepoTask;

                return new ApiResponse<InventoryDTO>(true, "", [], updatedEntity.Adapt<InventoryDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<IList<InventoryDTO>>> UpdateStockAsync(List<InventoryDTO> model)
        {
            try
            {
                var res = await _repository.UpdateStockAsync(model.Adapt<List<InventoryModel>>());
                if (res == null)
                    return new ApiResponse<IList<InventoryDTO>>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<IList<InventoryDTO>>(true, "", [], res.Adapt<List<InventoryDTO>>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
