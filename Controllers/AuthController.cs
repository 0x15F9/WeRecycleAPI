using System.Threading.Tasks;
using API.DTO;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IUploadService _upload;

        public AuthController(
            ILogger<AuthController> logger, 
            IAuthService auth, 
            IMapper mapper,
            IUploadService upload
        ) {
            _logger = logger;
            _auth = auth;
            _upload = upload;
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
            const string Path = "Drivers";
            dto.IdCard = await _upload.UploadImage(form.IdCard, Path);
            dto.ProofOfAddress = await _upload.UploadImage(form.ProofOfAddress, Path);
            dto.DrivingLicense = await _upload.UploadImage(form.DrivingLicense, Path);

            var driver = await _auth.RegisterDriver(dto);

            // TODO: catch collisions

            return Ok(driver);
        }

        // [HttpPatch]
        // public async Task<ActionResult<string>> UploadPhoto(IFormFile file, int driverId){
        //     // TODO: extract id from token to prevent misuse
        //     // use driverid to upload file to prevent multi upload
        //     // rename here itself
        //     return Ok(await _upload.UploadImage(file, "drivers", driverId.ToString()));
        // }
    }
}