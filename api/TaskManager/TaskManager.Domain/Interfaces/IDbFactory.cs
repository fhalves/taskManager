using System.Data.Common;
using System.Threading.Tasks;

namespace TaskManager.Domain.Interfaces
{
    public interface IDbFactory
    {
        Task<DbConnection> GetDbConnection();
    }
}
