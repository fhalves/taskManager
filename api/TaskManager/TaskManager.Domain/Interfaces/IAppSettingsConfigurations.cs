namespace TaskManager.Domain.Interfaces
{
    public interface IAppSettingsConfigurations
    {
        string GetConnectionString();

        string GetJwtKey();

        string GetJwtKeyExpirationTime();
    }
}
