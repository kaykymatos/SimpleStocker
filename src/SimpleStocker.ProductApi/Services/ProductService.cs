using Mapster;
using SimpleStocker.ProductApi.Caching;
using SimpleStocker.ProductApi.Caching.Services;
using SimpleStocker.ProductApi.DTO;
using SimpleStocker.ProductApi.Factories;
using SimpleStocker.ProductApi.Models;
using SimpleStocker.ProductApi.Repositories;
using SimpleStocker.ProductApi.Util;
using SimpleStocker.ProductApi.Validations;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace SimpleStocker.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICachingService _cache;
        private readonly IConfiguration _config;
        public ProductService(IProductRepository repository, ICachingService cache, IConfiguration config)
        {
            _repository = repository;
            _cache = cache;
            _config = config;
        }

        public async Task<ApiResponse<ProductDTO>> CreateAsync(ProductDTO model)
        {
            var validation = new ProductValidator().Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<ProductDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));
            try
            {
                var res = await _repository.CreateAsync(model.Adapt<ProductModel>());
                if (res == null)
                    return new ApiResponse<ProductDTO>("Server", "Erro ao tentar criar registro!");
                var responseWithQuantity = res.Adapt<ProductDTO>();
                responseWithQuantity.QuantityStock = model.QuantityStock;
                return new ApiResponse<ProductDTO>(true, "", [], responseWithQuantity, 200);
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
                    await _cache.RemoveAsync(string.Format(CacheKeys.GetOneProduct, id));
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

                    List<string> keyList = [];
                    foreach (var id in ids)
                        keyList.Add(string.Format(CacheKeys.GetOneProduct, id));

                    await _cache.RemoveListAsync(keyList);
                    return new ApiResponse<bool>(true, "", [], true, 200);
                }
                return new ApiResponse<bool>("Server", "Erro ao deletar itens");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<IList<ProductDTO>>> GetAllAsync()
        {
            var cachedProduct = await _cache.GetAsync(CacheKeys.GetAllProducts);
            if (!string.IsNullOrEmpty(cachedProduct))
            {
                var cachedResponse = new ApiResponse<IList<ProductDTO>>(JsonSerializer.Deserialize<List<ProductDTO>>(cachedProduct));
                return cachedResponse;
            }
            var dbReturn = await _repository.GetAllAsync();
            var response = new ApiResponse<IList<ProductDTO>>(dbReturn.Adapt<IList<ProductDTO>>());

            var httpClientFactoryService = new HttpClientFactory(new HttpClient() { BaseAddress = new Uri(_config["ExternalServicesUrls:InventorySerivce"]) });
            ApiResponse<List<InventoryDTO>> inventoryData = new ApiResponse<List<InventoryDTO>>();
            try
            {
                inventoryData = await httpClientFactoryService.PostAsync<List<InventoryDTO>>(
                    "/inventory/get-inventory-by-product-id-list",
                    response.Data.Select(x => x.Id).ToList()
                );
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            foreach (var item in response.Data)
                item.QuantityStock = inventoryData.Data.First(x => x.ProductId == item.Id).Quantity;

            await _cache.SetAsync(CacheKeys.GetAllProducts, JsonSerializer.Serialize(response.Data));

            return response;
        }

        public async Task<ApiResponse<ProductDTO>> GetOneAsync(long id)
        {
            var cachedProduct = await _cache.GetAsync(string.Format(CacheKeys.GetOneProduct, id));
            if (!string.IsNullOrEmpty(cachedProduct))
            {
                var cachedResponse = new ApiResponse<ProductDTO>(JsonSerializer.Deserialize<ProductDTO>(cachedProduct));
                return cachedResponse;
            }
            var response = new ApiResponse<ProductDTO>();
            response.Data = new ProductDTO();
            var dbReturn = await _repository.GetOneAsync(id);
            dbReturn.Adapt(response.Data);
            var httpClientFactoryService = new HttpClientFactory(new HttpClient() { BaseAddress = new Uri(_config["ExternalServicesUrls:InventorySerivce"]) });
            ApiResponse<List<InventoryDTO>> inventoryData = new ApiResponse<List<InventoryDTO>>();
            try
            {
                inventoryData = await httpClientFactoryService.PostAsync<List<InventoryDTO>>(
                    "/inventory/get-inventory-by-product-id-list",
                    new List<long> { id }
                );
                response.Data.QuantityStock = inventoryData.Data.First().Quantity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            await _cache.SetAsync(string.Format(CacheKeys.GetOneProduct, id), JsonSerializer.Serialize(response.Data));
            return response;
        }

        public async Task<ApiResponse<ProductDTO>> UpdateAsync(long id, ProductDTO model)
        {
            var originalmodel = await _repository.GetOneAsync(id);
            if (originalmodel == null)
                return new ApiResponse<ProductDTO>("Id", "Item não encontrado");

            var validation = new ProductValidator(true).Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<ProductDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                var products = JsonSerializer.Deserialize<List<ProductDTO>>(await _cache.GetAsync(CacheKeys.GetAllProducts));
                model.Adapt(originalmodel);

                var productUpdate = products.FirstOrDefault(x => x.Id == id);
                model.Adapt(productUpdate);
                productUpdate.UpdatedDate = DateTime.UtcNow;

                var updateRepoTask = _repository.UpdateAsync(originalmodel);
                var updateCacheTask = _cache.SetAsync(
                    CacheKeys.GetAllProducts,
                    JsonSerializer.Serialize(products.Adapt<List<ProductDTO>>())
                );
                var updateProducCacheTask = _cache.SetAsync(
                    string.Format(CacheKeys.GetOneProduct, id),
                    JsonSerializer.Serialize(productUpdate)
                );

                await Task.WhenAll(updateRepoTask, updateCacheTask, updateProducCacheTask);

                var updatedEntity = await updateRepoTask;

                return new ApiResponse<ProductDTO>(true, "", [], updatedEntity.Adapt<ProductDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
