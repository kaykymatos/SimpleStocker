using FluentValidation;
using SimpleStocker.Api.Models.Entities;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;
using SimpleStocker.Api.Util;

namespace SimpleStocker.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        protected readonly IValidator<ProductViewModel> _validator;
        public ProductService(IProductRepository repository, IValidator<ProductViewModel> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ApiResponse<ProductViewModel>> CreateAsync(ProductViewModel entity)
        {
            var validation = _validator.Validate(entity);
            if (!validation.IsValid)
                return new ApiResponse<ProductViewModel>(validation.Errors.Select(x => x.ErrorMessage).ToList());
            try
            {
                var mapperModel = Mapper.Map<Product>(entity);
                var res = await _repository.CreateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<ProductViewModel>(["Erro ao tentar criar registro!"]);
                return new ApiResponse<ProductViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ProductViewModel>> DeleteAsync(long id)
        {
            try
            {
                var foundEntity = await _repository.GetOneAsync(id);
                if (foundEntity == null)
                    return new ApiResponse<ProductViewModel>(["Id não encontrado!"]);

                var deleteItem = await _repository.DeleteAsync(foundEntity);
                if (deleteItem)
                    return new ApiResponse<ProductViewModel>(true, "", [], Mapper.Map<ProductViewModel>(foundEntity), 200);
                return new ApiResponse<ProductViewModel>(true, "", ["Erro ao deletar item"], Mapper.Map<ProductViewModel>(foundEntity), 400);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<List<ProductViewModel>>> GetAllAsync()
        {
            try
            {
                var foundEntity = await _repository.GetAllAsync();
                List<ProductViewModel> lista = [];
                foreach (var item in foundEntity)
                {
                    lista.Add(Mapper.Map<ProductViewModel>(item));
                }
                return new ApiResponse<List<ProductViewModel>>(true, "", [], lista, 200);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ProductViewModel>> GetOneAsync(long id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<ProductViewModel>(["Id não encontrado!"]);

                var mapperModel = Mapper.Map<ProductViewModel>(entity);

                return new ApiResponse<ProductViewModel>(true, "", [], mapperModel, 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ProductViewModel>> UpdateAsync(ProductViewModel entity)
        {
            var originalEntity = await _repository.GetOneAsync(entity.Id);
            if (originalEntity == null)
                return new ApiResponse<ProductViewModel>(["Item não encontrado"]);

            var validation = _validator.Validate(entity);
            if (!validation.IsValid)
                return new ApiResponse<ProductViewModel>(validation.Errors.Select(x => x.ErrorMessage).ToList());

            try
            {
                var mapperModel = Mapper.Map<Product>(entity);
                var res = await _repository.UpdateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<ProductViewModel>(["Erro ao tentar criar registro!"]);
                return new ApiResponse<ProductViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
