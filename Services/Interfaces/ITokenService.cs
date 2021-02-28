using System.Collections.Generic;
using System.Security.Claims;
using API.Models;

namespace API.Services
{
    public interface ITokenService
    {
        string Generate(User user);

        User Parse(IEnumerable<Claim> claims);
    }
}