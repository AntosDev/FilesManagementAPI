namespace Identity.Domain.User
{
    public interface IUserRepository
    {
        IdentityUser FindByUsername(string username);
    }
}
