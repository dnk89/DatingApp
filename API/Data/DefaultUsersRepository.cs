using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace API.Data
{
    public class DefaultUsersRepository : NpgsqlRepository, IUsersRepository
    {
        public DefaultUsersRepository(IConfiguration config) : base(config)
        {
        }

        public async Task AddUser(AppUser user)
        {
            using var conn = GetConnection();

            var sql = @"INSERT INTO ""Users"" (""UserName"", ""PasswordHash"", ""PasswordSalt"")";
            sql += @" VALUES (@UserName, @PasswordHash, @PasswordSalt)";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@UserName", user.UserName.ToUpper(), DbType.String, ParameterDirection.Input);
            parameter.Add("@PasswordHash", user.PasswordHash, DbType.Binary, ParameterDirection.Input);
            parameter.Add("@PasswordSalt", user.PasswordSalt, DbType.Binary, ParameterDirection.Input);

            await conn.ExecuteAsync(sql, parameter);
        }

        public async Task<AppUser> GetUser(int id)
        {
            using var conn = GetConnection();

            var sql = @"SELECT ""Id"", ""UserName"", ""PasswordHash"", ""PasswordSalt"" FROM ""Users""";
            sql += @" WHERE ""Id"" = @Id";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

            return await conn.QuerySingleOrDefaultAsync<AppUser>(sql, parameter);
        }

        public async Task<AppUser> GetUser(string username)
        {
            using var conn = GetConnection();

            var sql = @"SELECT ""Id"", ""UserName"", ""PasswordHash"", ""PasswordSalt"" FROM ""Users""";
            sql += @" WHERE ""UserName"" = @UserName";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@UserName", username.ToUpper(), DbType.String, ParameterDirection.Input);

            return await conn.QuerySingleOrDefaultAsync<AppUser>(sql, parameter);
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            using var conn = GetConnection();

            const string sql = @"SELECT ""Id"", ""UserName"", ""PasswordHash"", ""PasswordSalt"" FROM ""Users""";

            return await conn.QueryAsync<AppUser>(sql);
        }
    }
}