using System.Collections.Generic;
using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces.Repositories
{
    public interface ITaskRepository : IBaseRepository<Tasks>
    {
        IEnumerable<Tasks> GetByUser(int? userId);
    }
}
