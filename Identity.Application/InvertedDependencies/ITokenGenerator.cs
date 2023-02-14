using Identity.Domain;
using System.Security.Claims;

namespace Identity.Application.InvertedDependencies
{
    public interface ITokenGenerator
    {
        string BuildToken(string secret, IdentityUser userData,  Claim[] claim);
    }
}
