using Mapster;
using SimpleStocker.InventoryApi.DTO;
using SimpleStocker.InventoryApi.Models;
using SimpleStocker.InventoryApi.Repositories;
using SimpleStocker.InventoryApi.Util;
using SimpleStocker.InventoryApi.Validations;

namespace SimpleStocker.InventoryApi.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
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
                    return new ApiResponse<bool>(true, "", [], true, 200);
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
                var foundEntity = await _repository.GetAllAsync();

                return new ApiResponse<IList<InventoryDTO>>(true, "", [], foundEntity.Adapt<List<InventoryDTO>>(), 200);

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
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<InventoryDTO>("Id", "Id não encontrado!");

                return new ApiResponse<InventoryDTO>(true, "", [], entity.Adapt<InventoryDTO>(), 200);
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
                model.Adapt(originalmodel);
                var res = await _repository.UpdateAsync(id, originalmodel);
                if (res == null)
                    return new ApiResponse<InventoryDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<InventoryDTO>(true, "", [], res.Adapt<InventoryDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<List<InventoryDTO>>> UpdateStockAsync(List<InventoryDTO> model)
        {
            try
            {

                var res = await _repository.UpdateStockAsync(model.Adapt<List<InventoryModel>>());
                if (res == null)
                    return new ApiResponse<List<InventoryDTO>>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<List<InventoryDTO>>(true, "", [], res.Adapt<List<InventoryDTO>>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
