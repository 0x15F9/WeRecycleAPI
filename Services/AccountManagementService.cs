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
    public class AccountManagementService : IAccountManagementService
    {
        private readonly ILogger<AccountManagementService> _logger;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AccountManagementService(ILogger<AccountManagementService> logger, DataContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<DriverAccount> ChangeStatus(int driverId, UserStatus status)
        {
            Driver driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == driverId);
            driver.Status = status;
            await _context.SaveChangesAsync();
            return _mapper.Map<DriverAccount>(driver);
        }

        public async Task<DriverAccount> GetDriver(int driverId)
        {
            Driver driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == driverId);
            return _mapper.Map<DriverAccount>(driver);
        }

        public IEnumerable<DriverAccount> GetDrivers()
        {
            var drivers = _context.Drivers.AsAsyncEnumerable();
            return _mapper.Map<IEnumerable<DriverAccount>>(drivers);
        }
    }
}