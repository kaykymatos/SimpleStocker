using FluentValidation;
using SimpleStocker.Api.Models.Entities;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;
using SimpleStocker.Api.Util;

namespace SimpleStocker.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        protected readonly IValidator<CategoryViewModel> _validator;
        public CategoryService(ICategoryRepository repository, IValidator<CategoryViewModel> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ApiResponse<CategoryViewModel>> CreateAsync(CategoryViewModel entity)
        {
            var validation = _validator.Validate(entity);
            if (!validation.IsValid)
                return new ApiResponse<CategoryViewModel>(validation.Errors.Select(x => x.ErrorMessage).ToList());
            try
            {
                var mapperModel = Mapper.Map<Category>(entity);
                var res = await _repository.CreateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<CategoryViewModel>(["Erro ao tentar criar registro!"]);
                return new ApiResponse<CategoryViewModel>();
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
                    return new ApiResponse<CategoryViewModel>(["Id não encontrado!"]);
                var deleteItem = await _repository.DeleteAsync(foundEntity);
                if (deleteItem)
                    return new ApiResponse<CategoryViewModel>(true, "", [], Mapper.Map<CategoryViewModel>(foundEntity), 200);
                return new ApiResponse<CategoryViewModel>(true, "", ["Erro ao deletar item"], Mapper.Map<CategoryViewModel>(foundEntity), 400);

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
                    return new ApiResponse<CategoryViewModel>(["Id não encontrado!"]);

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
                return new ApiResponse<CategoryViewModel>(["Item não encontrado"]);

            var validation = _validator.Validate(entity);
            if (!validation.IsValid)
                return new ApiResponse<CategoryViewModel>(validation.Errors.Select(x => x.ErrorMessage).ToList());

            try
            {
                var mapperModel = Mapper.Map<Category>(entity);
                var res = await _repository.UpdateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<CategoryViewModel>(["Erro ao tentar criar registro!"]);
                return new ApiResponse<CategoryViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
