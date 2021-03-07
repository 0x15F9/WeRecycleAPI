using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Models;

namespace API.Services
{
    public interface IRouteService
    {
        IEnumerable<RouteRes> GetRoutes();
        Task<IEnumerable<RouteRes>> GetRoutes(int driverId);
        Task<RouteRes> CreateRoute(CreateRoute route, int driverId);
        Task<RouteRes> AddPickup(CreatePickup pickup);
    }
}