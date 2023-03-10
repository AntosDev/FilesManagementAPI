using Identity.Domain;
using MediatR;

namespace Identity.Application.UseCases.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<AuthenticationResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}