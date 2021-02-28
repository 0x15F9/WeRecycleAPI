using System.Threading.Tasks;
using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _auth;

        public AuthController(ILogger<AuthController> logger, IAuthService auth) {
            _logger = logger;
            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult<LoginRes>> Login(LoginReq login)
        {
            LoginRes res = await _auth.Login(login);
            return res == null ? Unauthorized() : Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<LoginRes>> RegisterDriver(CreateDriver driver)
        {
            var user = await _auth.RegisterDriver(driver);

            return user == null ? Unauthorized() : Ok(user);
        }


    }
}