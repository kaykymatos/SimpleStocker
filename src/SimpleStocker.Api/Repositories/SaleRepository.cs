using Dapper;
using SimpleStocker.Api.Context;
using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DapperContext _context;
        private readonly ISaleItemRepository _saleItemRepository;

        public SaleRepository(DapperContext context, ISaleItemRepository saleItemRepository)
        {
            _context = context;
            _saleItemRepository = saleItemRepository;
        }

        public async Task<Sale> CreateAsync(Sale entity)
        {
            try
            {
                var sql = "INSERT INTO Sales (CustomerId,TotalAmount,  Discount, PaymentMethod, Status)" +
                    " VALUES (@CustomerId,@TotalAmount,  @Discount, @PaymentMethod, @Status) RETURNING Id;";
                DynamicParameters parameters = new();
                parameters.Add("@CustomerId", entity.CustomerId);
                parameters.Add("@TotalAmount", entity.TotalAmount);
                parameters.Add("@Discount", entity.Discount);
                parameters.Add("@PaymentMethod", entity.PaymentMethod);
                parameters.Add("@Status", entity.Status);

                using var _db = _context.CreateConnection();
                var id = await _db.ExecuteScalarAsync<long>(sql, parameters);
                entity.Id = id;

                foreach (var item in entity.Items)
                {
                    item.SaleId = id;
                    await _saleItemRepository.CreateAsync(item);
                }

                entity.Id = id;

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(Sale entity)
        {
            try
            {
                await _saleItemRepository.DeleteBySaleId(entity.Id);
                var sql = "DELETE FROM Sales where Id = @Id";
                DynamicParameters parameters = new();
                parameters.Add("@Id", entity.Id);
                using var _db = _context.CreateConnection();
                await _db.ExecuteAsync(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM Sales ORDER BY ID;";
                using var _db = _context.CreateConnection();
                var sales = await _db.QueryAsync<Sale>(sql);

                foreach (var item in sales)
                    item.Items = await _saleItemRepository.GetAllSaleItemsBySaleId(item.Id);

                return [.. sales];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Sale> GetOneAsync(long id)
        {
            try
            {
                var sql = "SELECT * FROM Sales where Id = @Id";
                DynamicParameters parameters = new();
                parameters.Add("@Id", id);
                using var _db = _context.CreateConnection();
                var item = await _db.QueryFirstOrDefaultAsync<Sale>(sql, parameters);

                if (item != null)
                    item.Items = await _saleItemRepository.GetAllSaleItemsBySaleId(id);

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Sale> UpdateAsync(Sale entity)
        {
            try
            {
                var sql = "UPDATE Sales SET " +
                    "CustomerId = @CustomerId, " +
                    "Discount = @Discount, " +
                    "TotalAmount = @TotalAmount, " +
                    "PaymentMethod = @PaymentMethod , " +
                    "UpdatedDate = @UpdatedDate , " +
                    "Status = @Status " +
                   " where Id = @Id";

                DynamicParameters parameters = new();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@CustomerId", entity.CustomerId);
                parameters.Add("@Discount", entity.Discount);
                parameters.Add("@TotalAmount", entity.TotalAmount);
                parameters.Add("@PaymentMethod", entity.PaymentMethod);
                parameters.Add("@Status", entity.Status);
                parameters.Add("@UpdatedDate", DateTime.Now);
                using var _db = _context.CreateConnection();
                await _db.ExecuteAsync(sql, parameters);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task ClearDb()
        {
            var sql = "DELETE FROM Sales";
            await _saleItemRepository.ClearDb();
            using var _db = _context.CreateConnection();
            await _db.ExecuteAsync(sql);
        }

        public async Task<List<Sale>> GetAllSalesByClientId(long clientId)
        {
            try
            {
                var sql = "SELECT * FROM Sales where CustomerId = @CustomerId ORDER BY ID;";
                using var _db = _context.CreateConnection();
                DynamicParameters parameters = new();
                parameters.Add("@CustomerId", clientId);
                var sales = await _db.QueryAsync<Sale>(sql);

                foreach (var item in sales)
                    item.Items = await _saleItemRepository.GetAllSaleItemsBySaleId(item.Id);

                return [.. sales];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
