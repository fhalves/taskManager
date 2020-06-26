using System.Collections.Generic;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.Services;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services
{
    public class TaskService : ServiceBase, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserService _userService;


        public TaskService(ITaskRepository taskRepository,
            IUserService userService)
        {
            _taskRepository = taskRepository;
            _userService = userService;
        }

        public IEnumerable<Tasks> Get(int? userId)
        {
            if (userId == null)
                return _taskRepository.GetAll();

            return _taskRepository.GetByUser(userId);
        }

        public Tasks Get(int id)
        {
            return _taskRepository.Get(id);
        }

        public Tasks Post(Tasks task)
        {
            if (_userService.VerifyIfUserExists(task.UserId) == null)
            {
                AddNotification("UserId received is not from a valid user.");
                return task;
            }

            if (_taskRepository.Post(task) == null)
                AddNotification("Failed to include a new task.");

            return task;
        }

        public Tasks Put(Tasks task)
        {
            Tasks findTask = VerifyIfTaskExists(task.Id);
            if (findTask == null)
                return task;

            if (_taskRepository.Put(task) == null)
                AddNotification("Failed to update a task.");

            return task;
        }

        public Tasks Delete(int id)
        {
            Tasks findTask = VerifyIfTaskExists(id);
            if (findTask == null)
                return null;

            _taskRepository.Delete(findTask);

            return findTask;
        }

        private Tasks VerifyIfTaskExists(int id)
        {
            Tasks findTask = this.Get(id);
            if (findTask == null)
            {
                AddNotification("Task not found.");
                return null;
            }

            return findTask;
        }
    }
}
