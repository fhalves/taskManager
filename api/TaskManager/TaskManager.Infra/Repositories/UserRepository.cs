using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Models;
using TaskManager.Domain.Models.Request;
using TaskManager.Infra.Common;

namespace TaskManager.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public User GetAuthUser(AuthRequest auth)
        {
            base.CreateParameters();
            base.AddParameters(new DapperParameters { Name = "userLogin", Value = auth.User });

            string query = $@"SELECT * FROM USER WHERE LOGIN = @userLogin";

            return base.QueryFirstOrDefault<User>(query, base.GetParameters());
        }
    }
}
