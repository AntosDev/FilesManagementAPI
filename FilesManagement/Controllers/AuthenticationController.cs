using FilesManagement.DTOs;
using Identity.Application.UseCases.AuthenticateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            this._mediator = mediator ?? throw new ArgumentNullException();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(new AuthResultDTO()
                {
                    Message = "Invalid Payload"
                });
            }

            var authenticationResult = await _mediator.Send(new AuthenticateUserCommand
            {
                Password = loginDTO.Password,
                UserName = loginDTO.Username
            });
            return Ok(new AuthResultDTO { Message = authenticationResult.Message, AccessToken = authenticationResult.Token });

        }
    }
}
