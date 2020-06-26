using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Interfaces.Services;
using TaskManager.Domain.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseResponseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return CreaterResponse(_userService, _userService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return CreaterResponse(_userService, _userService.Get(id));
        }

        [HttpPost]
        [AllowAnonymous]

        public IActionResult Post([FromBody] User user)
        {
            return CreaterResponse(_userService, _userService.Post(user));
        }

        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            return CreaterResponse(_userService, _userService.Put(user));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return CreaterResponse(_userService, _userService.Delete(id));
        }
    }
}
