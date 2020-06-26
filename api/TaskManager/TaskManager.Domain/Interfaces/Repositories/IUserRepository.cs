using System;
using TaskManager.Domain.Models;
using TaskManager.Domain.Models.Request;

namespace TaskManager.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetAuthUser(AuthRequest auth);
    }
}
