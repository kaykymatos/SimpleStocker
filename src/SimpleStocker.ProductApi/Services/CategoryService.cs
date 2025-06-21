using Mapster;
using SimpleStocker.CategoryApi.Repositories;
using SimpleStocker.ProductApi.DTO;
using SimpleStocker.ProductApi.Models;
using SimpleStocker.ProductApi.Repositories;
using SimpleStocker.ProductApi.Services;
using SimpleStocker.ProductApi.Util;
using SimpleStocker.ProductApi.Validations;

namespace SimpleStocker.CategoryApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
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
                    return new ApiResponse<bool>(true, "", [], true, 200);
                return new ApiResponse<bool>("Server", "Erro ao deletar item");

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
                var foundEntity = await _repository.GetAllAsync();

                return new ApiResponse<IList<CategoryDTO>>(true, "", [], foundEntity.Adapt<List<CategoryDTO>>(), 200);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<CategoryDTO>> GetOneAsync(long id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<CategoryDTO>("Id", "Id não encontrado!");

                return new ApiResponse<CategoryDTO>(true, "", [], entity.Adapt<CategoryDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                model.Adapt(originalmodel);
                var res = await _repository.UpdateAsync(id, originalmodel);
                if (res == null)
                    return new ApiResponse<CategoryDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<CategoryDTO>(true, "", [], res.Adapt<CategoryDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
