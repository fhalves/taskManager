using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Interfaces.Services;
using TaskManager.Domain.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : BaseResponseController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult Get(int? userId)
        {
            return CreaterResponse(_taskService, _taskService.Get(userId));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return CreaterResponse(_taskService, _taskService.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tasks task)
        {
            return CreaterResponse(_taskService, _taskService.Post(task));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Tasks task)
        {
            return CreaterResponse(_taskService, _taskService.Put(task));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return CreaterResponse(_taskService, _taskService.Delete(id));
        }
    }
}