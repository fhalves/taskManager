using Microsoft.Extensions.Options;
using TaskManager.Domain.Interfaces;
using TaskManager.Infra.Common;

namespace TaskManager.Extensions
{
    public class AppSettingsConfigurations : IAppSettingsConfigurations
    {
        private readonly IOptions<ConnectionString> _appSettings;
        private readonly IOptions<JwtSecret> _appJwtSettings;

        public AppSettingsConfigurations(IOptions<ConnectionString> appSettings,
            IOptions<JwtSecret> appJwtSettings)
        {
            _appSettings = appSettings;
            _appJwtSettings = appJwtSettings;
        }

        public string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_appSettings?.Value?.DbTaskManagerConnectionString))
                return string.Empty;

            return _appSettings.Value.DbTaskManagerConnectionString;
        }

        public string GetJwtKey()
        {
            if (string.IsNullOrEmpty(_appJwtSettings?.Value?.Key))
                return string.Empty;

            return _appJwtSettings.Value.Key;
        }

        public string GetJwtKeyExpirationTime()
        {
            if (string.IsNullOrEmpty(_appJwtSettings?.Value?.ExpirationTime))
                return string.Empty;

            return _appJwtSettings.Value.ExpirationTime;
        }
    }
}
