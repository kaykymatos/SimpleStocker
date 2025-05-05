using FluentValidation;
using SimpleStocker.Api.Models.Entities;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;
using SimpleStocker.Api.Util;

namespace SimpleStocker.Api.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        protected readonly IValidator<SaleViewModel> _validator;
        public SaleService(ISaleRepository repository, IValidator<SaleViewModel> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ApiResponse<SaleViewModel>> CreateAsync(SaleViewModel entity)
        {
            var validation = _validator.Validate(entity);
            if (!validation.IsValid)
                return new ApiResponse<SaleViewModel>(validation.Errors.Select(x => x.ErrorMessage).ToList());
            try
            {
                var mapperModel = Mapper.Map<Sale>(entity);
                var res = await _repository.CreateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<SaleViewModel>(["Erro ao tentar criar registro!"]);
                return new ApiResponse<SaleViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<SaleViewModel>> DeleteAsync(long id)
        {
            try
            {
                var foundEntity = await _repository.GetOneAsync(id);
                if (foundEntity == null)
                    return new ApiResponse<SaleViewModel>(["Id não encontrado!"]);

                var deleteItem = await _repository.DeleteAsync(foundEntity);
                if (deleteItem)
                    return new ApiResponse<SaleViewModel>(true, "", [], Mapper.Map<SaleViewModel>(foundEntity), 200);
                return new ApiResponse<SaleViewModel>(true, "", ["Erro ao deletar item"], Mapper.Map<SaleViewModel>(foundEntity), 400);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<List<SaleViewModel>>> GetAllAsync()
        {
            try
            {
                var foundEntity = await _repository.GetAllAsync();
                List<SaleViewModel> lista = [];
                foreach (var item in foundEntity)
                {
                    lista.Add(Mapper.Map<SaleViewModel>(item));
                }
                return new ApiResponse<List<SaleViewModel>>(true, "", [], lista, 200);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<SaleViewModel>> GetOneAsync(long id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<SaleViewModel>(["Id não encontrado!"]);

                var mapperModel = Mapper.Map<SaleViewModel>(entity);

                return new ApiResponse<SaleViewModel>(true, "", [], mapperModel, 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<SaleViewModel>> UpdateAsync(SaleViewModel entity)
        {
            var originalEntity = await _repository.GetOneAsync(entity.Id);
            if (originalEntity == null)
                return new ApiResponse<SaleViewModel>(["Item não encontrado"]);

            var validation = _validator.Validate(entity);
            if (!validation.IsValid)
                return new ApiResponse<SaleViewModel>(validation.Errors.Select(x => x.ErrorMessage).ToList());

            try
            {
                var mapperModel = Mapper.Map<Sale>(entity);
                var res = await _repository.UpdateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<SaleViewModel>(["Erro ao tentar criar registro!"]);
                return new ApiResponse<SaleViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
