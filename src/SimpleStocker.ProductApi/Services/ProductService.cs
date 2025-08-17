using Mapster;
using SimpleStocker.ProductApi.DTO;
using SimpleStocker.ProductApi.Models;
using SimpleStocker.ProductApi.Repositories;
using SimpleStocker.ProductApi.Util;
using SimpleStocker.ProductApi.Validations;

namespace SimpleStocker.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
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
                    return new ApiResponse<bool>(true, "", [], true, 200);
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
                    return new ApiResponse<bool>(true, "", [], true, 200);
                return new ApiResponse<bool>("Server", "Erro ao deletar itens");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<IList<ProductDTO>>> GetAllAsync()
        {
            try
            {
                var foundEntity = await _repository.GetAllAsync();

                return new ApiResponse<IList<ProductDTO>>(true, "", [], foundEntity.Adapt<List<ProductDTO>>(), 200);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ProductDTO>> GetOneAsync(long id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<ProductDTO>("Id", "Id não encontrado!");

                return new ApiResponse<ProductDTO>(true, "", [], entity.Adapt<ProductDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                model.Adapt(originalmodel);
                var res = await _repository.UpdateAsync(originalmodel);
                if (res == null)
                    return new ApiResponse<ProductDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<ProductDTO>(true, "", [], res.Adapt<ProductDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
