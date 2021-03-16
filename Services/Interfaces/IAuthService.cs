using System.Threading.Tasks;
using API.DTO;
using API.Models;

namespace API.Services
{
    public interface IAuthService
    {
        // Register New Driver
        Task<LoginRes> RegisterDriver(CreateDriver dto);

        // Login User
        Task<LoginRes> Login(LoginReq req);

        // Reset Password
        Task<bool> UpdatePassword(User user, UpdatePassword dto);
    }
}