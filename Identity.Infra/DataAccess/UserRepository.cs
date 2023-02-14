using Identity.Domain;
using Identity.Domain.User;

namespace Identity.Infra.DataAccess
{
    internal class UserRepository : IUserRepository
    {
        public IdentityUser FindByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
