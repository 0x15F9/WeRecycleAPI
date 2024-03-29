using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    [ApiController]
    [Authorize(Roles = "ADMIN")]
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
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_bins.ReadBin());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetBinMaterials()
        {
            Dictionary<string, int> enumList = ((BinMaterial[])Enum.GetValues(typeof(BinMaterial))).ToDictionary(k => k.ToString(), v => (int)v);

            return Ok(enumList);
        }

        [HttpPost]
        public async Task<IActionResult> New(CreateBin dto)
        {
            return Ok(await _bins.CreateBin(dto));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return await _bins.DeleteBin(id) ? Ok("Deleted") : NotFound("Failed to delete");
        }

        [HttpPatch]
        public async Task<IActionResult> Update(UpdateBin dto)
        {
            return Ok(await _bins.UpdateBin(dto));
        }
    }
}