using Dapper;
using SimpleStocker.Api.Context;
using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.Api.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DapperContext _context;

        public ClientRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Client> CreateAsync(Client entity)
        {
            try
            {
                var sql = "INSERT INTO Clients (Name, Email, PhoneNumer, Address, AddressNumber, Active, BirthDate)" +
                    " VALUES (@Name, @Email, @PhoneNumer, @Address, @AddressNumber, @Active, @BirthDate) RETURNING Id;";
                DynamicParameters parameters = new();
                parameters.Add("@Name", entity.Name);
                parameters.Add("@Email", entity.Email);
                parameters.Add("@PhoneNumer", entity.PhoneNumer);
                parameters.Add("@Address", entity.Address);
                parameters.Add("@AddressNumber", entity.AddressNumber);
                parameters.Add("@Active", entity.Active);
                parameters.Add("@BirthDate", entity.BirthDate);
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

        public async Task<bool> DeleteAsync(Client entity)
        {
            try
            {
                var sql = "DELETE FROM Clients where Id = @Id ORDER BY ID;";
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

        public async Task<List<Client>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM Clients ORDER BY ID;";
                using var _db = _context.CreateConnection();
                var clients = await _db.QueryAsync<Client>(sql);
                return [.. clients];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Client> GetOneAsync(long id)
        {
            try
            {
                var sql = "SELECT * FROM Clients where Id = @Id";
                DynamicParameters parameters = new();
                parameters.Add("@Id", id);
                using var _db = _context.CreateConnection();
                return await _db.QueryFirstOrDefaultAsync<Client>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Client> UpdateAsync(Client entity)
        {
            try
            {
                var sql = "UPDATE Clients SET " +
                    "Name = @Name, " +
                    "Email = @Email, " +
                    "PhoneNumer = @PhoneNumer, " +
                    "Address = @Address , " +
                    "AddressNumber = @AddressNumber, " +
                    "Active = @Active, " +
                    "UpdatedDate = @UpdatedDate, " +
                    "BirthDate = @BirthDate " +
                    "where Id = @Id";

                DynamicParameters parameters = new();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@Name", entity.Name);
                parameters.Add("@Email", entity.Email);
                parameters.Add("@PhoneNumer", entity.PhoneNumer);
                parameters.Add("@Address", entity.Address);
                parameters.Add("@AddressNumber", entity.AddressNumber);
                parameters.Add("@Active", entity.Active);
                parameters.Add("@BirthDate", entity.BirthDate);
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
            var sql = "delete from Clients";

            using var _db = _context.CreateConnection();
            await _db.ExecuteAsync(sql);
        }
    }
}
