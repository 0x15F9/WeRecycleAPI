using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthService _auth;
        private readonly IMapper _mapper;
        private readonly IUploadService _upload;
        private readonly IAccountManagementService _account;
        private readonly ITokenService _token;

        public AccountController(
            ILogger<AccountController> logger, 
            IAuthService auth, 
            IMapper mapper,
            IUploadService upload,
            IAccountManagementService account,
            ITokenService token
        ) {
            _logger = logger;
            _auth = auth;
            _upload = upload;
            _mapper = mapper;
            _account = account;
            _token = token;
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

            return driver == null ? Conflict() :Ok(driver);
        }

        [HttpGet]
        [Authorize(Roles = "DRIVER")]
        public async Task<ActionResult<IEnumerable<DriverAccount>>> Driver()
        {
            // get driver id from token
            User user = await _token.Parse(User.Claims);
            return Ok(await _account.GetDriver(user.Id));
        }


        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<AdminAccount>>> Admin()
        {
            // get admin id from token
            User user = await _token.Parse(User.Claims);
            return Ok(await _account.GetAdmin(user.Id));
        }

        // View drivers as admin
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<IEnumerable<DriverAccount>> Drivers(){
            return Ok(_account.GetDrivers());
        }

        // Validate driver as admin
        [HttpPatch]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<DriverAccount>> UpdateStatus(int driverId, UserStatus newStatus) {
            return Ok(await _account.ChangeStatus(driverId, newStatus));
        }

        [HttpPatch]
        [Authorize(Roles = "ADMIN, DRIVER")]
        public async Task<ActionResult<bool>> UpdatePassword(UpdatePassword dto)
        {
            User user = await _token.Parse(User.Claims);
            return Ok(await _auth.UpdatePassword(user, dto));
        }
    }
}