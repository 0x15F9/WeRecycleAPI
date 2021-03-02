using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Models;

namespace API.Services
{
    public interface IAccountManagementService
    {
        IEnumerable<DriverAccount> GetDrivers();
        Task<DriverAccount> ChangeStatus(int driverId, UserStatus status);

        Task<DriverAccount> GetDriver(int driverId);
    }
}