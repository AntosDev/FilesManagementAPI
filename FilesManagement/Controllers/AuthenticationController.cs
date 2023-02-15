using FilesManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public void Login(LoginRequestDTO loginDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Unauthorized(new AuthResultDTO()
            //    {
            //        Success = false,
            //        Errors = new List<string> { "Invalid Payload" }
            //    });
            //}


            //LogedInUserBE userBE;
            //string jwtToken;
            //GetTokenAndUserData(loginDTO, out userBE, out jwtToken);
            //LogedinUserDTO userDTO = mapToUserDTO(userBE);
            //return Ok(new AuthResultDTO()
            //{
            //    Success = true,
            //    Token = jwtToken,
            //    User = userDTO
            //});
            
        }
    }
}
