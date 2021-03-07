using System.Threading.Tasks;
using API.DTO;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _route;
        private readonly ITokenService _token;
        private readonly IMapper _mapper;

        public RoutesController(IRouteService route, ITokenService token, IMapper mapper)
        {
            _route = route;
            _token = token;
            _mapper = mapper;
        }
        
        // Add Route as driver
        [HttpPost]
        [Authorize(Roles = "DRIVER", Policy="MustBeApproved")]
        public async Task<ActionResult> New([FromForm] CreateRoute newRoute){
            // get driver from token
            User user = await  _token.Parse(User.Claims);

            RouteRes res = await _route.CreateRoute(newRoute, user.Id);
            return Ok(res);
        }

        [HttpPatch]
        [Authorize(Roles = "DRIVER", Policy = "MustBeApproved")]
        public async Task<ActionResult> AddPickup([FromForm] CreatePickup newPickup)
        {
            // get driver from token
            User user = await _token.Parse(User.Claims);

            // TODO: ensure route belongs to driver

            RouteRes res = await _route.AddPickup(newPickup);
            return Ok(res);
        }

        [HttpGet]
        // [Authorize(Roles = "DRIVER", Policy = "MustBeApproved")]
        public ActionResult All()
        {
            return Ok(_route.GetRoutes());
        }

        // View routes as driver and as admin

    }
}