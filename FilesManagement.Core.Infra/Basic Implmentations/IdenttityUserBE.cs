using System;
using System.Collections.Generic;
using System.Text;

namespace SSWEB.Identity.BusinessEntities
{
    public class IdentityUserBE
    {
        public string EntityID { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
        public long ID { get; set; }
        public bool Enabled { get; set; }
    }
}
