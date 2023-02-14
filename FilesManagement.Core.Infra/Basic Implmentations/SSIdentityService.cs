using SSWEB.CrossCuttingConcerns.Services.Interfaces;
using SSWEB.Identity.BusinessEntities;
using SSWEB.Identity.DAL.DALogic;
using SSWEB.Identity.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SSWEB.Identity.Services
{
    public class SSIdentityService : ISSIdentityService
    {
        IIdentityUserDAL usersDAL;
        ISSTokenService tokenService;
        ISSPasswordHasher passwordHasher;

        public SSIdentityService(ISSTokenService _tokenService, ISSPasswordHasher _passwordHasher, IIdentityUserDAL identityUsersDAL)
        {
            usersDAL = identityUsersDAL;
            tokenService = _tokenService;
            passwordHasher = _passwordHasher;
        }
        public string Login(string username, string password, string secret, out LogedInUserBE user, string vendorRoleEntityId)
        {
            var jwtToken = string.Empty;
            user = new LogedInUserBE();
            IdentityUserBE existingUser = AuthenticateUserOrFail(username, password);
            user = GetUserData(existingUser, vendorRoleEntityId);
            jwtToken = tokenService.BuildToken(secret, user);

            return jwtToken;
        }

        public LogedInUserBE BasicLogin(string username, string password, string vendorRoleEntityId)
        {
            var user = new LogedInUserBE();
            IdentityUserBE existingUser = AuthenticateUserOrFail(username, password);
            user = GetUserData(existingUser, vendorRoleEntityId);
            return user;
        }

        private IdentityUserBE AuthenticateUserOrFail(string username, string password)
        {
            var existingUser = usersDAL.GetUserByUsername(username);
            if (existingUser == null)
            {
                throw new ISSIdentityService.WrongCredentialsException();
            }

            var isCorrect = CheckPassword(existingUser, password);

            if (!isCorrect)
            {
                throw new ISSIdentityService.WrongCredentialsException();
            }
            if (!existingUser.Enabled)
            {
                throw new ISSIdentityService.UserDisabledException();
            }

            return existingUser;
        }

        public List<RolePermissionBE> GetUserPermissions(long userID)
        {
            var userPermissions = usersDAL.GetUserRolesPermissions(userID);
            userPermissions = FilterPermissions(userPermissions);
            return userPermissions;
        }
        public LogedInUserBE GetUserData(IdentityUserBE existingUser, string vendorRoleEntityId)
        {

            var userRoles = usersDAL.GetUserRolesIDs(existingUser.ID);

            var user = new LogedInUserBE
            {
                EntityID = existingUser.EntityID,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                ID = existingUser.ID,
                Password = existingUser.Password,
                Username = existingUser.Username,
                UserPermissions = new List<RolePermissionBE>(),
                RolesEntityIDs = userRoles,
                IsVendor = userRoles.Contains(vendorRoleEntityId)
            };
            user.UserPermissions.AddRange(GetUserPermissions(existingUser.ID));
            return user;
        }

        private List<RolePermissionBE> FilterPermissions(List<RolePermissionBE> userPermissions)
        {
            userPermissions = userPermissions.Where(p => p.HasAccess()).ToList();
            //var withDuplicates = userPermissions.GroupBy(x => x.ResourceDefinitionEntityID)
            //  .Where(g => g.Count() > 1).Select(gr => gr.Key);

            //withDuplicates.ForEach(resDefEntityID => 
            //{
            //    var correspondingResources = userPermissions.Where(p => p.ResourceDefinitionEntityID == resDefEntityID).ToList();
            //    RolePermissionBE newRolePermissionBE = new RolePermissionBE
            //    {
            //        ResourceDefinitionEntityID = correspondingResources[0].ResourceDefinitionEntityID,
            //        ModuleEnabled = correspondingResources[0].ModuleEnabled,
            //        ParentResourceDefnitionEntityID = correspondingResources[0].ParentResourceDefnitionEntityID,
            //        ModuleEntityID = correspondingResources[0].ModuleEntityID,
            //        ResourceDefinitionName = correspondingResources[0].ResourceDefinitionName,
            //        RolePermissionEntityID = correspondingResources[0].RolePermissionEntityID
            //    };
            //    correspondingResources.ForEach
            //})
            return userPermissions;
        }

        private bool CheckPassword(IdentityUserBE user, string password)
        {
            var checkResult = passwordHasher.Check(user.Password, password);

            return checkResult.Verified;
        }

        //TODO: Set in a decryption helper
        private string DecryptPass(string pass)
        {
            //TODO: Decryption algo
            return pass;
        }

    }
}
