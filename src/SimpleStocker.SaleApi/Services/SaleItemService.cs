using Mapster;
using SimpleStocker.SaleApi.DTO;
using SimpleStocker.SaleApi.Models.Entities;
using SimpleStocker.SaleApi.Repositories;
using SimpleStocker.SaleApi.Util;
using SimpleStocker.SaleApi.Validations;

namespace SimpleStocker.SaleApi.Services
{
    public class SaleItemService : ISaleItemService
    {
        private readonly ISaleItemRepository _repository;

        public SaleItemService(ISaleItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<SaleItemDTO>> CreateAsync(SaleItemDTO model)
        {
            var validation = new SaleItemValidator().Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<SaleItemDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));
            try
            {
                var res = await _repository.CreateAsync(model.Adapt<SaleItemModel>());
                if (res == null)
                    return new ApiResponse<SaleItemDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<SaleItemDTO>(true, "", [], res.Adapt<SaleItemDTO>(), 200);
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

        public async Task<ApiResponse<IList<SaleItemDTO>>> GetAllAsync()
        {
            try
            {
                var foundEntity = await _repository.GetAllAsync();

                return new ApiResponse<IList<SaleItemDTO>>(true, "", [], foundEntity.Adapt<List<SaleItemDTO>>(), 200);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<SaleItemDTO>> GetOneAsync(long id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<SaleItemDTO>("Id", "Id não encontrado!");

                return new ApiResponse<SaleItemDTO>(true, "", [], entity.Adapt<SaleItemDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<SaleItemDTO>> UpdateAsync(long id, SaleItemDTO model)
        {
            var originalmodel = await _repository.GetOneAsync(id);
            if (originalmodel == null)
                return new ApiResponse<SaleItemDTO>("Id", "Item não encontrado");

            var validation = new SaleItemValidator().Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<SaleItemDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                model.Adapt(originalmodel);
                var res = await _repository.UpdateAsync(id, originalmodel);
                if (res == null)
                    return new ApiResponse<SaleItemDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<SaleItemDTO>(true, "", [], res.Adapt<SaleItemDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
