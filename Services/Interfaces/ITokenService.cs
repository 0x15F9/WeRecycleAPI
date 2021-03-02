using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    public interface ITokenService
    {
        string Generate(User user);

        Task<User> Parse(IEnumerable<Claim> claims);
    }
}