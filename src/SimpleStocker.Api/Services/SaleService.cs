using FluentValidation;
using FluentValidation.Results;
using SimpleStocker.Api.Models.Entities;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;
using SimpleStocker.Api.Util;
using SimpleStocker.Api.Validations;

namespace SimpleStocker.Api.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        public SaleService(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task ClearDb()
        {
            await _repository.ClearDb();
        }
        public async Task<ApiResponse<SaleViewModel>> CreateAsync(SaleViewModel entity)
        {
            entity.TotalAmount = entity.Items.Sum(x => x.SubTotal) - entity.Discount;
            // Validação
            var validation = new SaleValidator().Validate(entity);
            if (!validation.IsValid)
                return new ApiResponse<SaleViewModel>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                // Mapeia SaleViewModel para Sale
                var sale = Mapper.Map<Sale>(entity);
                sale.Items = entity.Items
                    .Select(item => Mapper.Map<SaleItem>(item))
                    .ToList();


                // Persiste a venda
                var result = await _repository.CreateAsync(sale);
                if (result == null)
                    return new ApiResponse<SaleViewModel>("Server", "Erro ao tentar criar registro!");

                // Mapeia resultado de volta para ViewModel
                var saleViewModel = Mapper.Map<SaleViewModel>(result);
                saleViewModel.Items = result.Items
                    .Select(item => Mapper.Map<SaleItemViewModel>(item))
                    .ToList();

                return new ApiResponse<SaleViewModel>(true, "", [], saleViewModel, 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); // (Dica: Evite capturar exceções só para relançar — ou logue o erro, ou remova o catch)
            }
        }


        public async Task<ApiResponse<SaleViewModel>> DeleteAsync(long id)
        {
            try
            {
                var foundEntity = await _repository.GetOneAsync(id);
                if (foundEntity == null)
                    return new ApiResponse<SaleViewModel>("Id", "Id não encontrado!");

                var deleteItem = await _repository.DeleteAsync(foundEntity);
                if (deleteItem)
                    return new ApiResponse<SaleViewModel>(true, "", [], Mapper.Map<SaleViewModel>(foundEntity), 200);
                return new ApiResponse<SaleViewModel>("Server", "Erro ao deletar item");

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
                    var sale = Mapper.Map<SaleViewModel>(item);
                    sale.Items = [];
                    foreach (var item2 in item.Items)
                        sale.Items.Add(Mapper.Map<SaleItemViewModel>(item2));

                    lista.Add(sale);
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
                var foundEntity = await _repository.GetOneAsync(id);
                if (foundEntity == null)
                    return new ApiResponse<SaleViewModel>("Id", "Id não encontrado!");

                var mapperModel = Mapper.Map<SaleViewModel>(foundEntity);

                mapperModel.Items = [];
                foreach (var item2 in foundEntity.Items)
                    mapperModel.Items.Add(Mapper.Map<SaleItemViewModel>(item2));

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
                return new ApiResponse<SaleViewModel>("Id", "Item não encontrado");

            var validation = new SaleValidator(true).Validate(entity);

            if (!validation.IsValid)
                return new ApiResponse<SaleViewModel>(ErrorFormater.FulentValidationResultToDictionaryList(validation));

            try
            {
                var mapperModel = Mapper.Map<Sale>(entity);

                var res = await _repository.UpdateAsync(mapperModel);
                if (res == null)
                    return new ApiResponse<SaleViewModel>("Server", "Erro ao tentar criar registro!");
                return new ApiResponse<SaleViewModel>(true, "", [], Mapper.Map<SaleViewModel>(res), 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
