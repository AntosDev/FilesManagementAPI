using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSWEB.Identity.BusinessEntities
{
    public class LogedInUserBE
    {
        public string EntityID { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
        public long ID { get; set; }

        public List<string> RolesEntityIDs { get; set; }
        public List<RolePermissionBE> UserPermissions { get; set; }
        public bool IsVendor { get; set; }
    }
}
