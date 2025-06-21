using Mapster;
using SimpleStocker.ClientApi.DTO;
using SimpleStocker.ClientApi.Models;
using SimpleStocker.ClientApi.Repositories;
using SimpleStocker.ClientApi.Util;
using SimpleStocker.ClientApi.Validations;

namespace SimpleStocker.ClientApi.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<ClientDTO>> CreateAsync(ClientDTO model)
        {
            var validation = new ClientValidator().Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<ClientDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));
            try
            {
                var res = await _repository.CreateAsync(model.Adapt<ClientModel>());
                if (res == null)
                    return new ApiResponse<ClientDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<ClientDTO>(true, "", [], res.Adapt<ClientDTO>(), 200);
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

        public async Task<ApiResponse<IList<ClientDTO>>> GetAllAsync()
        {
            try
            {
                var foundEntity = await _repository.GetAllAsync();

                return new ApiResponse<IList<ClientDTO>>(true, "", [], foundEntity.Adapt<List<ClientDTO>>(), 200);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ClientDTO>> GetOneAsync(long id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<ClientDTO>("Id", "Id não encontrado!");

                return new ApiResponse<ClientDTO>(true, "", [], entity.Adapt<ClientDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ClientDTO>> UpdateAsync(long id, ClientDTO model)
        {
            var originalmodel = await _repository.GetOneAsync(id);
            if (originalmodel == null)
                return new ApiResponse<ClientDTO>("Id", "Item não encontrado");

            var validation = new ClientValidator(true).Validate(model);

            if (!validation.IsValid)
                return new ApiResponse<ClientDTO>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                model.Adapt(originalmodel);
                var res = await _repository.UpdateAsync(id, originalmodel);
                if (res == null)
                    return new ApiResponse<ClientDTO>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<ClientDTO>(true, "", [], res.Adapt<ClientDTO>(), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
