using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Infra.Factory
{
    public class DbFactory : IDbFactory
    {
        private readonly IAppSettingsConfigurations _appSettings;
        private readonly string connectionString;

        public DbFactory(IAppSettingsConfigurations appSettings, string conString = null)
        {
            _appSettings = appSettings;
            connectionString = _appSettings.GetConnectionString();
        }

        public async Task<DbConnection> GetDbConnection()
        {
            DbConnection connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();

                return connection;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
