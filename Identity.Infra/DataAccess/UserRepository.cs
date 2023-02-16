using Identity.Domain;
using Identity.Domain.User;
using Identity.Infra.DataAccess.Context;
using Identity.Infra.DataAccess.Entities;
using System.Linq;

namespace Identity.Infra.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDBContext dbContext;

        public UserRepository(IdentityDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IdentityUser FindByUsername(string username)
        {
            var dbEntities = this.dbContext.Users.Where(u => u.Username == username);
            if (dbEntities == null || dbEntities.Count() == 0) throw new InvalidOperationException("Resource not found");
            return dbEntities.Select(u => ToAggregate(u)).First();
        }

        private static IdentityUser ToAggregate(UserEntity dbEntity)
        {
            if (dbEntity == null) throw new Exception("Mapping for null failed");
            return new IdentityUser
            {
                Username = dbEntity.Username,
                Password = dbEntity.Password,
                FirstName = dbEntity.FirstName,
                LastName = dbEntity.LastName,
                EntityID = dbEntity.UserId
            };
        }
    }
}
