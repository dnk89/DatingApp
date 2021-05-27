using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace API.Data
{
    public abstract class NpgsqlRepository
    {
        private readonly string _connectionString;

        protected NpgsqlRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        protected IDbConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);  
            conn.Open();  
            return conn;
        }
    }
}