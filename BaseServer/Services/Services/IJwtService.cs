using Entities;
using Entities.User;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
        Task<IEnumerable<Claim>> ReadClaimsAsync(string token);
    }
}