using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.Services;
using TaskManager.Domain.Models;
using TaskManager.Domain.Models.Request;

namespace TaskManager.Application.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> Get()
        {
            return _userRepository.GetAll();
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public User Post(User user)
        {
            if (this.Get()?.ToList().Find(u => u.Login == user.Login) != null)
            {
                AddNotification("An user with the same login is already in use. Please try other username.");
                return user;
            }

            user.EncryptPassword();
            if (_userRepository.Post(user) == null)
                AddNotification("Failed to add a new user.");

            return user;
        }

        public User Put(User user)
        {
            User findUser = VerifyIfUserExists(user.Id);
            if (findUser == null)
                return user;

            user.EncryptPassword();
            if (_userRepository.Put(user) == null)
                AddNotification("Failed to update an user.");

            return user;
        }

        public User Delete(int id)
        {
            User findUser = VerifyIfUserExists(id);
            if (findUser == null)
                return null;

            _userRepository.Delete(findUser);

            return findUser;
        }

        public User GetAuthUser(AuthRequest auth)
        {
            return _userRepository.GetAuthUser(auth);
        }

        public User VerifyIfUserExists(int id)
        {
            User findUser = this.Get(id);
            if (findUser == null)
            {
                AddNotification("User not found.");
                return null;
            }

            return findUser;
        }
    }
}
