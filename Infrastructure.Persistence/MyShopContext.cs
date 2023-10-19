using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;


namespace Infrastructure.Persistence
{
    public class MyShopContext : DbContext, IMyShopContext
    {
        private readonly IConfiguration _configuration;

        public MyShopContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_configuration.GetConnectionString("MyshopContext"));

        public IDbConnection CreateMasterConnection()
            => new NpgsqlConnection(_configuration.GetConnectionString("MasterConnection"));

        public void CreateDatabase(string dbName)
        {
            using (var connection = CreateMasterConnection())
            {
                var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{dbName}'";
                var dbCount = connection.ExecuteScalar<int>(sqlDbCount);
                if (dbCount == 0)
                {
                    connection.Execute($"CREATE DATABASE \"{dbName}\"");
                }

            }
        }
    }
}
