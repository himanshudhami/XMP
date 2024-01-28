using System.Data;
using Npgsql;

namespace YourProjectName.Infrastructure.Data
{
    public class DapperDbContext
    {
        private readonly string _connectionString;

        public DapperDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
