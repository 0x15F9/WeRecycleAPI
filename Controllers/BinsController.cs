using System.Threading.Tasks;
using API.DTO;
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
        private readonly IBinService _bins;

        public BinsController(ILogger<BinsController> logger, IBinService bins)
        {
            _logger = logger;
            _bins = bins;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bins.ReadBin());
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> New(CreateBin dto)
        {
            return Ok(await _bins.CreateBin(dto));
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _bins.DeleteBin(id) ? Ok("Deleted") : NotFound("Failed to delete");
        }

        [HttpPatch]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(UpdateBin dto)
        {
            return Ok(await _bins.UpdateBin(dto));
        }
    }
}