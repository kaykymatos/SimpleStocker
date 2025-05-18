using SimpleStocker.Api.Models.Entities;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;
using SimpleStocker.Api.Util;
using SimpleStocker.Api.Validations;

namespace SimpleStocker.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly ISaleRepository _salesRepository;
        public ClientService(IClientRepository repository, ISaleRepository salesRepository)
        {
            _repository = repository;
            _salesRepository = salesRepository;
        }

        public async Task ClearDb()
        {
            await _repository.ClearDb();
        }
        public async Task<ApiResponse<ClientViewModel>> CreateAsync(ClientViewModel entity)
        {
            var validation = new ClientValidator().Validate(entity);

            if (!validation.IsValid)
                return new ApiResponse<ClientViewModel>(ErrorFormater.FulentValidationResultToDictionaryList(validation));
            try
            {
                var mapperModel = Mapper.Map<Client>(entity);
                var res = await _repository.CreateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<ClientViewModel>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<ClientViewModel>(true, "", [], Mapper.Map<ClientViewModel>(res), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ClientViewModel>> DeleteAsync(long id)
        {
            try
            {
                var foundEntity = await _repository.GetOneAsync(id);
                if (foundEntity == null)
                    return new ApiResponse<ClientViewModel>("Id", "Id não encontrado!");

                var sales = await _salesRepository.GetAllSalesByClientId(id);
                if (sales.Count > 0)
                    return new ApiResponse<ClientViewModel>("Sale", "Este cliente tem vendas vinculadas!");

                var deleteItem = await _repository.DeleteAsync(foundEntity);
                if (deleteItem)
                    return new ApiResponse<ClientViewModel>(true, "", [], Mapper.Map<ClientViewModel>(foundEntity), 200);
                return new ApiResponse<ClientViewModel>("Server", "Erro ao deletar item");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<List<ClientViewModel>>> GetAllAsync()
        {
            try
            {
                var foundEntity = await _repository.GetAllAsync();
                List<ClientViewModel> lista = [];
                foreach (var item in foundEntity)
                {
                    lista.Add(Mapper.Map<ClientViewModel>(item));
                }
                return new ApiResponse<List<ClientViewModel>>(true, "", [], lista, 200);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ClientViewModel>> GetOneAsync(long id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(id);
                if (entity == null)
                    return new ApiResponse<ClientViewModel>("Id", "Id não encontrado!");

                var mapperModel = Mapper.Map<ClientViewModel>(entity);

                return new ApiResponse<ClientViewModel>(true, "", [], mapperModel, 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse<ClientViewModel>> UpdateAsync(ClientViewModel entity)
        {
            var originalEntity = await _repository.GetOneAsync(entity.Id);
            if (originalEntity == null)
                return new ApiResponse<ClientViewModel>("Id", "Item não encontrado");

            var validation = new ClientValidator(true).Validate(entity);

            if (!validation.IsValid)
                return new ApiResponse<ClientViewModel>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                var mapperModel = Mapper.Map<Client>(entity);
                var res = await _repository.UpdateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<ClientViewModel>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<ClientViewModel>(true, "", [], Mapper.Map<ClientViewModel>(res), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
