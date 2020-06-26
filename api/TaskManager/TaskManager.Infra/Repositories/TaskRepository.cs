using System.Collections.Generic;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Models;
using TaskManager.Infra.Common;

namespace TaskManager.Infra.Repositories
{
    public class TaskRepository : BaseRepository<Tasks>, ITaskRepository
    {
        public TaskRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<Tasks> GetByUser(int? userId)
        {
            base.CreateParameters();
            base.AddParameters(new DapperParameters { Name = "userId", Value = userId });
            string query = $@"SELECT * FROM TASK WHERE USERID = @userId";

            return base.Query<Tasks>(query, base.GetParameters());
        }
    }
}
