using System.Threading.Tasks;
using API.DTO;
using API.Services;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthController(ILogger<AuthController> logger, IAuthService auth, IMapper mapper) {
            _logger = logger;
            _auth = auth;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<LoginRes>> Login(LoginReq login)
        {
            LoginRes res = await _auth.Login(login);
            return res == null ? Unauthorized() : Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<LoginRes>> RegisterDriver([FromForm]CreateDriverForm form)
        {

            CreateDriver dto =  _mapper.Map<CreateDriver>(form);
            // save files and get names
            // append image paths

            var driver = await _auth.RegisterDriver(dto);
    



            return driver == null ? Unauthorized() : Ok(driver);
        }


    }
}