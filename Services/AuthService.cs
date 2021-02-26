using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AuthService(
            ILogger<AuthService> logger, 
            DataContext context, 
            IMapper mapper
        )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<LoginRes> Login(LoginReq req)
        {
            throw new System.NotImplementedException();
        }

        public async Task<LoginRes> RegisterDriver(CreateDriver driverDto)
        {
            if (await UserExists(driverDto.PhoneNumber)) return null;

            Driver driver = _mapper.Map<Driver>(driverDto);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(driverDto.Password, out passwordHash, out passwordSalt);
            driver.PasswordHash = passwordHash;
            driver.PasswordSalt = passwordSalt;
            driver.Status = UserStatus.PENDING;

            await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();

            LoginRes response = _mapper.Map<LoginRes>(driver);
            // TODO: add token

            return response;
        }

        public async Task<bool> UserExists(string phoneNumber) {
            return await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                // Create hash using password salt.
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                { // Loop through the byte array
                    if (computedHash[i] != passwordHash[i]) return false; // if mismatch
                }
            }
            return true; //if no mismatches.
        }
    }
}