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
                    " VALUES (@Name, @Description, @QuantityStock, @UnityOfMeasurement, @Price, @CategoryId) RETURNING Id;";
                DynamicParameters parameters = new();
                parameters.Add("@Name", entity.Name);
                parameters.Add("@Description", entity.Description);
                parameters.Add("@QuantityStock", entity.QuantityStock);
                parameters.Add("@UnityOfMeasurement", entity.UnityOfMeasurement);
                parameters.Add("@Price", entity.Price);
                parameters.Add("@CategoryId", entity.CategoryId);

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

        public async Task<bool> DeleteAsync(Product entity)
        {
            try
            {
                var sql = "DELETE FROM Products where Id = @Id";
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
                var sql = "SELECT prod.*, cat.Name as CategoryName FROM Products as prod left join Categories cat on prod.CategoryId = cat.Id ORDER BY prod.ID;";
                using var _db = _context.CreateConnection();
                var products = await _db.QueryAsync<Product>(sql);
                return [.. products];
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
                var sql = "SELECT prod.*, cat.Name as CategoryName FROM Products as prod left join Categories cat on prod.CategoryId = cat.Id where prod.Id = @Id";
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
                    "UnityOfMeasurement = @UnityOfMeasurement, " +
                    "Price = @Price, " +
                    "UpdatedDate = @UpdatedDate, " +
                    "CategoryId = @CategoryId " +
                   " where Id = @Id";

                DynamicParameters parameters = new();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@Name", entity.Name);
                parameters.Add("@Description", entity.Description);
                parameters.Add("@QuantityStock", entity.QuantityStock);
                parameters.Add("@UnityOfMeasurement", entity.UnityOfMeasurement);
                parameters.Add("@Price", entity.Price);
                parameters.Add("@CategoryId", entity.CategoryId);
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
            var sql = "TRUNCATE TABLE Products RESTART IDENTITY CASCADE";

            using var _db = _context.CreateConnection();
            var res = await _db.ExecuteAsync(sql);
        }

        public async Task<List<Product>> GetAllTasksByCategoryId(long categoryId)
        {
            try
            {
                var sql = "SELECT prod.*, cat.Name as CategoryName FROM Products as prod left join Categories cat on prod.CategoryId = cat.Id  where CategoryId = @CategoryId ORDER BY ID;";
                using var _db = _context.CreateConnection();
                DynamicParameters parameters = new();
                parameters.Add("@CategoryId", categoryId);
                var Products = await _db.QueryAsync<Product>(sql, parameters);
                return [.. Products];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
