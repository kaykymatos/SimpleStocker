using Dapper;
using SimpleStocker.Api.Context;
using SimpleStocker.Api.Models.Entities;
using static Dapper.SqlMapper;

namespace SimpleStocker.Api.Repositories
{
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly DapperContext _context;

        public SaleItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task ClearDb()
        {
            var sql = "delete from SaleItems";

            using var _db = _context.CreateConnection();
            await _db.ExecuteAsync(sql);
        }

        public async Task<SaleItem> CreateAsync(SaleItem entity)
        {
            try
            {
                var sql = "INSERT INTO SaleItems (SaleId,ProductId,Quantity,UnityPrice) VALUES (@SaleId,@ProductId,@Quantity,@UnityPrice) RETURNING Id;";
                DynamicParameters parameters = new();
                parameters.Add("@SaleId", entity.SaleId);
                parameters.Add("@ProductId", entity.ProductId);
                parameters.Add("@Quantity", entity.Quantity);
                parameters.Add("@UnityPrice", entity.UnityPrice);
                using var _db = _context.CreateConnection();
                var id = await _db.ExecuteScalarAsync<long>(sql, parameters);

                entity.Id = id;
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(SaleItem entity)
        {
            try
            {
                var sql = "DELETE FROM SaleItems where Id = @Id";
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

        public async Task<bool> DeleteBySaleId(long saleId)
        {
            try
            {
                var sql = "DELETE FROM SaleItems where SaleId = @SaleId";
                DynamicParameters parameters = new();
                parameters.Add("@SaleId", saleId);
                using var _db = _context.CreateConnection();
                await _db.ExecuteAsync(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SaleItem>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM SaleItems ORDER BY ID;";
                using var _db = _context.CreateConnection();
                var saleItems = await _db.QueryAsync<SaleItem>(sql);
                return [.. saleItems];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SaleItem>> GetAllSaleItemsBySaleId(long saleId)
        {
            try
            {
                var sql = "SELECT * FROM SaleItems where SaleId = @SaleId ORDER BY ID;";
                DynamicParameters parameters = new();
                parameters.Add("@SaleId", saleId);
                using var _db = _context.CreateConnection();
                var saleItems = await _db.QueryAsync<SaleItem>(sql, parameters);
                return [.. saleItems];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SaleItem>> GetAllSaleItemsByProductId(long productId)
        {
            try
            {
                var sql = "SELECT * FROM SaleItems where ProductId = @ProductId ORDER BY ID;";
                DynamicParameters parameters = new();
                parameters.Add("@ProductId", productId);
                using var _db = _context.CreateConnection();
                var saleItems = await _db.QueryAsync<SaleItem>(sql, parameters);
                return [.. saleItems];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SaleItem> GetOneAsync(long id)
        {
            try
            {
                var sql = "SELECT * FROM SaleItems where Id = @Id";
                DynamicParameters parameters = new();
                parameters.Add("@Id", id);
                using var _db = _context.CreateConnection();
                return await _db.QueryFirstOrDefaultAsync<SaleItem>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SaleItem> UpdateAsync(SaleItem entity)
        {
            try
            {
                var sql = "UPDATE SaleItems SET " +
                    "SaleId = @SaleId," +
                    "ProductId = @ProductId," +
                    "Quantity = @Quantity," +
                    "UnityPrice = @UnityPrice," +
                    "UpdatedDate = @UpdatedDate " +
                    "WHERE Id = @Id";
                DynamicParameters parameters = new();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@SaleId", entity.SaleId);
                parameters.Add("@ProductId", entity.ProductId);
                parameters.Add("@Quantity", entity.Quantity);
                parameters.Add("@UnityPrice", entity.UnityPrice);
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
    }
}
