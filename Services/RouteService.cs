using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class RouteService : IRouteService
    {
        private readonly ILogger<RouteService> _logger;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploader;

        public RouteService(
            ILogger<RouteService> logger,
            DataContext context,
            IMapper mapper,
            IUploadService uploader
        )
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _uploader = uploader;
        }

        public async Task<RouteRes> AddPickup(CreatePickup newPickup)
        {
            Route route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == newPickup.RouteId);
            Pickup pickup = _mapper.Map<Pickup>(newPickup);
            pickup.Route = route;

            // Upload image and extract path
            const string Path = "Bins";
            pickup.BeforeImage = await _uploader.UploadImage(newPickup.BeforeImage, Path);
            pickup.AfterImage = await _uploader.UploadImage(newPickup.AfterImage, Path);

            await _context.Pickups.AddAsync(pickup);
            await _context.SaveChangesAsync();
            return _mapper.Map<RouteRes>(route);
        }

        public async Task<RouteRes> CreateRoute(CreateRoute newRoute, int driverId)
        {
            // create the route
            Route route = _mapper.Map<Route>(newRoute);
            route.Driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == driverId);
            await _context.Routes.AddAsync(route);
            await _context.SaveChangesAsync();

            // use the route to call add pickup
            CreatePickup pickup = _mapper.Map<CreatePickup>(route);
            pickup.BeforeImage = newRoute.BeforeImage;
            pickup.AfterImage = newRoute.AfterImage;
            pickup.RouteId = route.Id;
            return await AddPickup(pickup);
        }

        public IEnumerable<RouteRes> GetRoutes()
        {
            IEnumerable<Route> routes = _context.Routes
                .Include(r => r.Pickups)
                .Include(r => r.Driver);
            return _mapper.Map<IEnumerable<RouteRes>>(routes);
        }

        public Task<IEnumerable<RouteRes>> GetRoutes(int driverId)
        {
            throw new System.NotImplementedException();
        }
    }
}