using System;
using System.Collections.Generic;
using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces.Services
{
    public interface ITaskService : IServiceBase
    {
        IEnumerable<Tasks> Get(int? userId);

        Tasks Get(int id);

        Tasks Post(Tasks task);

        Tasks Put(Tasks task);

        Tasks Delete(int id);
    }
}
