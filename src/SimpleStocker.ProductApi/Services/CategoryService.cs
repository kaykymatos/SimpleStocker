using Mapster;
using SimpleStocker.Caching;
using SimpleStocker.Caching.Services;
using SimpleStocker.ProductApi.DTO;
using SimpleStocker.ProductApi.Models;
using SimpleStocker.ProductApi.Repositories;
using SimpleStocker.ProductApi.Util;
using SimpleStocker.ProductApi.Validations;
using SimpleStocker.Shared.Models.Models;
using System.Text.Json;

namespace SimpleStocker.ProductApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ICachingService _cache;

        public CategoryService(ICategoryRepository repository, ICachingService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<ApiResponse<CategoryDTO>> CreateAsync(CategoryDTO model)
        {
            var validation = new CategoryValidator().Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<CategoryDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));
            try
            {
                var res = await _repository.CreateAsync(model.Adapt<CategoryModel>());
                if (res == null)
                    return new ApiResponse<CategoryDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<CategoryDTO>(true, "", [], res.Adapt<CategoryDTO>(), 200);
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
                    await _cache.RemoveAsync(string.Format(CacheKeys.GetOneCategory, id));
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
                    await _cache.RemoveListAsync(ids.Select(id => string.Format(CacheKeys.GetOneCategory, id)).ToList());
                    return new ApiResponse<bool>(true, "", [], true, 200);
                }
                return new ApiResponse<bool>("Server", "Erro ao deletar itens");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<IList<CategoryDTO>>> GetAllAsync()
        {
            try
            {
                var cachedCategories = await _cache.GetAsync(CacheKeys.GetAllCategories);
                if (!string.IsNullOrEmpty(cachedCategories))
                {
                    var cachedResponse = new ApiResponse<IList<CategoryDTO>>(JsonSerializer.Deserialize<List<CategoryDTO>>(cachedCategories));
                    return cachedResponse;
                }
                var dbReturn = await _repository.GetAllAsync();
                var response = new ApiResponse<IList<CategoryDTO>>(dbReturn.Adapt<IList<CategoryDTO>>());
                await _cache.SetAsync(CacheKeys.GetAllCategories, JsonSerializer.Serialize(response.Data));
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ApiResponse<CategoryDTO>> GetOneAsync(long id)
        {
            try
            {
                var cachedCategory = await _cache.GetAsync(string.Format(CacheKeys.GetOneCategory, id));
                if (!string.IsNullOrEmpty(cachedCategory))
                {
                    var cachedResponse = new ApiResponse<CategoryDTO>(JsonSerializer.Deserialize<CategoryDTO>(cachedCategory));
                    return cachedResponse;
                }
                var dbReturn = await _repository.GetOneAsync(id);
                var response = new ApiResponse<CategoryDTO>(dbReturn.Adapt<CategoryDTO>());

                await _cache.SetAsync(string.Format(CacheKeys.GetOneCategory, id), JsonSerializer.Serialize(response.Data));
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ApiResponse<CategoryDTO>> UpdateAsync(long id, CategoryDTO model)
        {
            var originalmodel = await _repository.GetOneAsync(id);
            if (originalmodel == null)
                return new ApiResponse<CategoryDTO>("Id", "Item não encontrado");

            var validation = new CategoryValidator().Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<CategoryDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                var categories = JsonSerializer.Deserialize<List<CategoryDTO>>(await _cache.GetAsync(CacheKeys.GetAllCategories));
                model.Adapt(originalmodel);

                var categoryUpdate = categories.FirstOrDefault(x => x.Id == id);
                model.Adapt(categoryUpdate);
                categoryUpdate.UpdatedDate = DateTime.UtcNow;

                var updateRepoTask = _repository.UpdateAsync(originalmodel);
                var updateCacheTask = _cache.SetAsync(
                    CacheKeys.GetAllCategories,
                    JsonSerializer.Serialize(categories.Adapt<List<CategoryDTO>>())
                );
                var updateProductCacheTask = _cache.SetAsync(
                    string.Format(CacheKeys.GetOneCategory, id),
                    JsonSerializer.Serialize(categoryUpdate)
                );

                await Task.WhenAll(updateRepoTask, updateCacheTask, updateProductCacheTask);

                var updatedEntity = await updateRepoTask;

                return new ApiResponse<CategoryDTO>(true, "", [], updatedEntity.Adapt<CategoryDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
