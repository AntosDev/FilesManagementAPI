using Identity.Domain;
using MediatR;

namespace Identity.Application.UseCases
{
    internal class AuthenticateUserCommand : IRequest<AuthenticationResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}