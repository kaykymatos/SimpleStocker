using Dapper;
using SimpleStocker.Api.Context;
using SimpleStocker.Api.Models.Entities;
using static Dapper.SqlMapper;

namespace SimpleStocker.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _context;

        public CategoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task ClearDb()
        {
            var sql = "TRUNCATE TABLE Categories RESTART IDENTITY CASCADE";

            using var _db = _context.CreateConnection();
            await _db.ExecuteAsync(sql);
        }

        public async Task<Category> CreateAsync(Category entity)
        {
            try
            {
                var sql = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description) RETURNING Id;";
                DynamicParameters parameters = new();
                parameters.Add("@Name", entity.Name);
                parameters.Add("@Description", entity.Description);
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

        public async Task<bool> DeleteAsync(Category entity)
        {
            try
            {
                var sql = "DELETE FROM Categories where Id = @Id ORDER BY ID;";
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

        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM Categories ORDER BY ID;";
                using var _db = _context.CreateConnection();
                var categories = await _db.QueryAsync<Category>(sql);
                return [.. categories];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetOneAsync(long id)
        {
            try
            {
                var sql = "SELECT * FROM Categories where Id = @Id;";
                DynamicParameters parameters = new();
                parameters.Add("@Id", id);
                using var _db = _context.CreateConnection();
                return await _db.QueryFirstOrDefaultAsync<Category>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            try
            {
                var sql = "UPDATE Categories SET Name = @Name, Description = @Description, UpdatedDate=@UpdatedDate WHERE Id = @Id";
                DynamicParameters parameters = new();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@Name", entity.Name);
                parameters.Add("@Description", entity.Description);
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
