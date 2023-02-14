using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesManagement.Core.Infra.Services
{
    internal class TokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 30;

        ISSCaching cachingService;

        public SSTokenService(ISSCaching cachingService)
        {
            this.cachingService = cachingService;
        }

        public string BuildToken(string secret, LogedInUserBE userData, string issuer = null)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secret);

            Claim[] claims = BuildClaims(userData);

            //var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
            //    expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var writtenToken = jwtTokenHandler.WriteToken(token);

            return writtenToken;
        }

        public void SavePermissionsToClaim(List<RolePermissionBE> permissions, List<Claim> claims, string userEntityID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            permissions.ForEach(urp =>
            {
                var resource = urp.ResourceDefinitionCode;
                if (urp.Import)
                {
                    stringBuilder.Append("||" + $"{resource}:{PermissionsLevel.Import.ToString()}");
                }
                if (urp.Delete)
                {
                    stringBuilder.Append("||" + $"{resource}:{PermissionsLevel.Delete.ToString()}");
                }
                if (urp.Export)
                {
                    stringBuilder.Append("||" + $"{resource}:{PermissionsLevel.Export.ToString()}");
                }
                if (urp.GetRecord)
                {
                    stringBuilder.Append("||" + $"{resource}:{PermissionsLevel.GetRecord.ToString()}");
                }
                if (urp.Insert)
                {
                    stringBuilder.Append("||" + $"{resource}:{PermissionsLevel.Insert.ToString()}");
                }
                if (urp.List)
                {
                    stringBuilder.Append("||" + $"{resource}:{PermissionsLevel.List.ToString()}");
                }
                if (urp.Update)
                {
                    stringBuilder.Append("||" + $"{resource}:{PermissionsLevel.Update.ToString()}");
                }
                if (urp.View)
                {
                    stringBuilder.Append("||" + $"{resource}:{PermissionsLevel.View.ToString()}");
                }
            });
            var cachingKey = "permissions" + userEntityID;
            cachingService.Set(cachingKey, stringBuilder.ToString());
            claims.Add(new Claim("permission", cachingKey));

        }
        public Claim[] BuildClaims(LogedInUserBE userData)
        {
            List<Claim> claims = new List<Claim>();

            SavePermissionsToClaim(userData.UserPermissions, claims, userData.EntityID);

            claims.Add(new Claim("userroles", string.Join('|', userData.RolesEntityIDs)));
            claims.Add(new Claim(ClaimTypes.Name, userData.Username));
            claims.Add(new Claim(ClaimTypes.GivenName, userData.LastName));
            claims.Add(new Claim("UserEntityId", userData.EntityID));
            claims.Add(new Claim("UserId", userData.ID.ToString()));
            claims.Add(new Claim("IsVendor", userData.IsVendor.ToString()));


            return claims.ToArray();
        }

        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
