using System.Collections.Generic;
using TaskManager.Domain.Models;
using TaskManager.Domain.Models.Request;

namespace TaskManager.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase
    {
        IEnumerable<User> Get();

        User Get(int id);

        User GetAuthUser(AuthRequest auth);

        User Post(User user);

        User Put(User user);

        User Delete(int id);

        /// <summary>
        /// This function verify if an user exists. If user will not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User VerifyIfUserExists(int id);
    }
}
