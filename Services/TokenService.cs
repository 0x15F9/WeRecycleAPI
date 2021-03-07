using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;

        public TokenService(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        public string Generate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName+ " "+ user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Status", user.Status.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_config["JWT:ExpireDays"]));

            var token = new JwtSecurityToken(
                _config["JWT:ValidIssuer"],
                _config["JWT:ValidAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> Parse(IEnumerable<Claim> claims)
        {
            int id = int.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            UserRole role = (UserRole)UserRole.Parse(typeof(UserRole), claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);

            switch (role)
            {
                case UserRole.ADMIN:
                    return await _context.Admins.FirstOrDefaultAsync(u => u.Id == id);

                case UserRole.DRIVER:
                    return await _context.Drivers.FirstOrDefaultAsync(u => u.Id == id);

                default:
                    return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            }

        }
    }
}