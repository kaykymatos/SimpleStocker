using Dapper;
using SimpleStocker.Api.Context;
using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DapperContext _context;

        public SaleRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale entity)
        {
            try
            {
                var sql = "INSERT INTO Sales (CustomerId, TotalAmount, Discount, PaymentMethod, Status)" +
                    " VALUES (@CustomerId, @TotalAmount, @Discount, @PaymentMethod, @Status)";
                DynamicParameters parameters = new();
                parameters.Add("@CustomerId", entity.CustomerId);
                parameters.Add("@TotalAmount", entity.TotalAmount);
                parameters.Add("@Discount", entity.Discount);
                parameters.Add("@PaymentMethod", entity.PaymentMethod);
                parameters.Add("@Status", entity.Status);

                using var _db = _context.CreateConnection();
                await _db.ExecuteAsync(sql, parameters);
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
                var sql = "SELECT * FROM Sales where Id = @Id";
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
                var sql = "SELECT * FROM Sales";
                using var _db = _context.CreateConnection();
                var Sales = await _db.QueryAsync<Sale>(sql);
                return [.. Sales];
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
                return await _db.QueryFirstOrDefaultAsync<Sale>(sql, parameters);
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
                    "TotalAmount = @TotalAmount, " +
                    "Discount = @Discount, " +
                    "PaymentMethod = @PaymentMethod , " +
                    "Status = @Status " +
                   " where Id = @Id";

                DynamicParameters parameters = new();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@CustomerId", entity.CustomerId);
                parameters.Add("@TotalAmount", entity.TotalAmount);
                parameters.Add("@Discount", entity.Discount);
                parameters.Add("@PaymentMethod", entity.PaymentMethod);
                parameters.Add("@Status", entity.Status);
                using var _db = _context.CreateConnection();
                await _db.ExecuteAsync(sql, parameters);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
