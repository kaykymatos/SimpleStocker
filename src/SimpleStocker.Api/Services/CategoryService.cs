using FluentValidation;
using FluentValidation.Results;
using SimpleStocker.Api.Models.Entities;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;
using SimpleStocker.Api.Util;
using SimpleStocker.Api.Validations;

namespace SimpleStocker.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IProductRepository _productRepository;
        public CategoryService(ICategoryRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<CategoryViewModel>> CreateAsync(CategoryViewModel entity)
        {
            var validation = new CategoryValidator().Validate(entity);
          
            if (!validation.IsValid)
                return new ApiResponse<CategoryViewModel>(ErrorFormater.FulentValidationResultToDictionaryList(validation));
            try
            {
                var mapperModel = Mapper.Map<Category>(entity);
                var res = await _repository.CreateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<CategoryViewModel>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<CategoryViewModel>(true, "", [], Mapper.Map<CategoryViewModel>(res), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<CategoryViewModel>> DeleteAsync(long id)
        {
            try
            {
                var foundEntity = await _repository.GetOneAsync(id);
                if (foundEntity == null)
                    return new ApiResponse<CategoryViewModel>("Id", "Id não encontrado!" );

                var products = await _productRepository.GetAllTasksByCategoryId(id);
                if(products.Count > 0)
                    return new ApiResponse<CategoryViewModel>("Product", "Existem produtos vinculados a essa categoria!");

                var deleteItem = await _repository.DeleteAsync(foundEntity);
                if (deleteItem)
                    return new ApiResponse<CategoryViewModel>(true, "", [], Mapper.Map<CategoryViewModel>(foundEntity), 200);
                return new ApiResponse<CategoryViewModel>("Server", "Erro ao deletar item");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<List<CategoryViewModel>>> GetAllAsync()
        {
            try
            {
                var foundEntity = await _repository.GetAllAsync();
                List<CategoryViewModel> lista = [];
                foreach (var item in foundEntity)
                {
                    lista.Add(Mapper.Map<CategoryViewModel>(item));
                }
                return new ApiResponse<List<CategoryViewModel>>(true, "", [], lista, 200);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<CategoryViewModel>> GetOneAsync(long id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<CategoryViewModel>("Id", "Id não encontrado!");

                var mapperModel = Mapper.Map<CategoryViewModel>(entity);

                return new ApiResponse<CategoryViewModel>(true, "", [], mapperModel, 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<CategoryViewModel>> UpdateAsync(CategoryViewModel entity)
        {
            var originalEntity = await _repository.GetOneAsync(entity.Id);
            if (originalEntity == null)
                return new ApiResponse<CategoryViewModel>("Id", "Item não encontrado");

            var validation = new CategoryValidator(true).Validate(entity);
           
            if (!validation.IsValid)
                return new ApiResponse<CategoryViewModel>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                var mapperModel = Mapper.Map<Category>(entity);
                var res = await _repository.UpdateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<CategoryViewModel>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<CategoryViewModel>(true, "", [], Mapper.Map<CategoryViewModel>(res),200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
