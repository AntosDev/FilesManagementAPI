using Identity.Domain.InvertedDependencies;

namespace Identity.Domain
{
    public class IdentityUser
    {
        public string EntityID { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }        
    }
}
