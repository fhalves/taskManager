using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Interfaces.Services;
using TaskManager.Infra.Common;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseResponseController : ControllerBase
    {
        private DefaultAPIResponse _response;

        public BaseResponseController()
        {
            _response = new DefaultAPIResponse();
        }

        [NonAction]
        protected IActionResult CreaterResponse(IServiceBase service, object data)
        {
            _response.Data = data;
            _response.Success = service.HasNotifications() ? false : true;
            _response.Notifications = service.GetNotifications();

            if (!_response.Success)
                return BadRequest(_response);

            return Ok(_response);
        }
    }
}