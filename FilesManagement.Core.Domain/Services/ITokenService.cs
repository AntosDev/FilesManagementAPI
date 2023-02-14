using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesManagement.Core.Domain.Services
{
    internal interface ITokenService
    {
        string BuildToken(string secret, LogedInUserBE userData, string issuer = null);
        bool IsTokenValid(string key, string issuer, string token);
        System.Security.Claims.Claim[] BuildClaims(LogedInUserBE userData);
    }
}
