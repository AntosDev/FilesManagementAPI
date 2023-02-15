﻿using Identity.Application.InvertedDependencies;
using Identity.Domain;
using Identity.Domain.InvertedDependencies;
using Identity.Domain.User;
using MediatR;
using System.Security.Claims;

namespace Identity.Application.UseCases.AuthenticateUser
{
    internal class AuthenticateUser : IRequestHandler<AuthenticateUserCommand, AuthenticationResult>
    {
        private readonly IUserRepository userRepo;
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenGenerator tokenGenerator;

        public AuthenticateUser(IUserRepository userRepo, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            this.userRepo = userRepo;
            this.passwordHasher = passwordHasher;
            this.tokenGenerator = tokenGenerator;
        }

        public Task<AuthenticationResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = this.userRepo.FindByUsername(request.UserName);
            this.ValidatePassword(user, request.Password);
            var token = this.tokenGenerator.BuildToken("very secret", user, BuildClaims(user));
            return Task.FromResult(new AuthenticationResult
            {
                AccessToken = token,
                Message = "Valid credentials",
            });
        }

        private bool ValidatePassword(IdentityUser user, string password)
        {
            var checkResult = passwordHasher.Check(user.Password, password);
            if (!checkResult.Verified) throw new UnauthorizedAccessException();

            if (checkResult.NeedsUpgrade)
            {
                //Raise password hash update requested event asynchronously
            }
        }

        public Claim[] BuildClaims(IdentityUser userData)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, userData.Username));
            claims.Add(new Claim(ClaimTypes.GivenName, userData.LastName));
            claims.Add(new Claim("UserEntityId", userData.EntityID));

            return claims.ToArray();
        }

    }
}