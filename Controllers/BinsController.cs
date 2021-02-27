using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BinsController : ControllerBase
    {
        private readonly ILogger<BinsController> _logger;
        private readonly ITokenService _service;
        public BinsController(ILogger<BinsController> logger, ITokenService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "DRIVER", Policy = "MustBeAppproved")]
        public IActionResult Driver()
        {
            return Ok("Ok");
        }
    }
}