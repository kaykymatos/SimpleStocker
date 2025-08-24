using Mapster;
using SimpleStocker.SaleApi.DTO;
using SimpleStocker.SaleApi.Models;
using SimpleStocker.SaleApi.Repositories;
using SimpleStocker.SaleApi.Util;
using SimpleStocker.SaleApi.Validations;

namespace SimpleStocker.SaleApi.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        public SaleService(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<SaleDTO>> CreateAsync(SaleDTO model)
        {
            var validation = new SaleValidator().Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<SaleDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));
            try
            {
                var res = await _repository.CreateAsync(model.Adapt<SaleModel>());
                if (res == null)
                    return new ApiResponse<SaleDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<SaleDTO>(true, "", [], res.Adapt<SaleDTO>(), 200);
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

        public async Task<ApiResponse<IList<SaleDTO>>> GetAllAsync()
        {
            try
            {
                var foundEntity = await _repository.GetAllAsync();

                return new ApiResponse<IList<SaleDTO>>(true, "", [], foundEntity.Adapt<List<SaleDTO>>(), 200);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<SaleDTO>> GetOneAsync(long id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<SaleDTO>("Id", "Id não encontrado!");

                return new ApiResponse<SaleDTO>(true, "", [], entity.Adapt<SaleDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<SaleDTO>> UpdateAsync(long id, SaleDTO model)
        {
            var originalmodel = await _repository.GetOneAsync(id);
            if (originalmodel == null)
                return new ApiResponse<SaleDTO>("Id", "Item não encontrado");

            var validation = new SaleValidator(true).Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<SaleDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                model.Items = originalmodel.Items.Adapt<List<SaleItemDTO>>();
                model.Adapt(originalmodel);
                var res = await _repository.UpdateAsync(id, originalmodel);
                if (res == null)
                    return new ApiResponse<SaleDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<SaleDTO>(true, "", [], res.Adapt<SaleDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
