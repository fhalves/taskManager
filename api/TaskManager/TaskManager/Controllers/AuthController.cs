using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Interfaces.Services;
using TaskManager.Domain.Models.Request;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseResponseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AuthRequest auth)
        {
            return CreaterResponse(_authService, _authService.Get(auth));
        }
    }
}