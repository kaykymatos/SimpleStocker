using Dapper;
using SimpleStocker.Api.Context;
using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;

        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            try
            {
                var sql = "INSERT INTO Products (Name, Description, QuantityStock, UnityOfMeasurement, Price, CategoryId)" +
                    " VALUES (@Name, @Description, @QuantityStock, @UnityOfMeasurement, @Price, @CategoryId)";
                DynamicParameters parameters = new();
                parameters.Add("@Name", entity.Name);
                parameters.Add("@Email", entity.Description);
                parameters.Add("@QuantityStock", entity.QuantityStock);
                parameters.Add("@UnityOfMeasurement", entity.UnityOfMeasurement);
                parameters.Add("@Price", entity.Price);
                parameters.Add("@CategoryId", entity.CategoryId);

                using var _db = _context.CreateConnection();
                await _db.ExecuteAsync(sql, parameters);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(Product entity)
        {
            try
            {
                var sql = "SELECT * FROM Products where Id = @Id";
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

        public async Task<List<Product>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM Products";
                using var _db = _context.CreateConnection();
                var Products = await _db.QueryAsync<Product>(sql);
                return [.. Products];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetOneAsync(long id)
        {
            try
            {
                var sql = "SELECT * FROM Products where Id = @Id";
                DynamicParameters parameters = new();
                parameters.Add("@Id", id);
                using var _db = _context.CreateConnection();
                return await _db.QueryFirstOrDefaultAsync<Product>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            try
            {
                var sql = "UPDATE Products SET " +
                    "Name = @Name, " +
                    "Description = @Description, " +
                    "QuantityStock = @QuantityStock, " +
                    "UnityOfMeasurement = @UnityOfMeasurement , " +
                    "Price = @Price, " +
                    "CategoryId = @CategoryId " +
                   " where Id = @Id";

                DynamicParameters parameters = new();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@Name", entity.Name);
                parameters.Add("@Email", entity.Description);
                parameters.Add("@QuantityStock", entity.QuantityStock);
                parameters.Add("@UnityOfMeasurement", entity.UnityOfMeasurement);
                parameters.Add("@Price", entity.Price);
                parameters.Add("@CategoryId", entity.CategoryId);
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
