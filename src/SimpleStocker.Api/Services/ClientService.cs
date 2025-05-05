using FluentValidation;
using SimpleStocker.Api.Models.Entities;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;
using SimpleStocker.Api.Util;

namespace SimpleStocker.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        protected readonly IValidator<ClientViewModel> _validator;
        public ClientService(IClientRepository repository, IValidator<ClientViewModel> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ApiResponse<ClientViewModel>> CreateAsync(ClientViewModel entity)
        {
            var validation = _validator.Validate(entity);
            if (!validation.IsValid)
                return new ApiResponse<ClientViewModel>(validation.Errors.Select(x => x.ErrorMessage).ToList());
            try
            {
                var mapperModel = Mapper.Map<Client>(entity);
                var res = await _repository.CreateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<ClientViewModel>(["Erro ao tentar criar registro!"]);
                return new ApiResponse<ClientViewModel>();
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
                    return new ApiResponse<ClientViewModel>(["Id não encontrado!"]);

                var deleteItem = await _repository.DeleteAsync(foundEntity);
                if (deleteItem)
                    return new ApiResponse<ClientViewModel>(true, "", [], Mapper.Map<ClientViewModel>(foundEntity), 200);
                return new ApiResponse<ClientViewModel>(true, "", ["Erro ao deletar item"], Mapper.Map<ClientViewModel>(foundEntity), 400);

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
                    return new ApiResponse<ClientViewModel>(["Id não encontrado!"]);

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
                return new ApiResponse<ClientViewModel>(["Item não encontrado"]);

            var validation = _validator.Validate(entity);
            if (!validation.IsValid)
                return new ApiResponse<ClientViewModel>(validation.Errors.Select(x => x.ErrorMessage).ToList());

            try
            {
                var mapperModel = Mapper.Map<Client>(entity);
                var res = await _repository.UpdateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<ClientViewModel>(["Erro ao tentar criar registro!"]);
                return new ApiResponse<ClientViewModel>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
