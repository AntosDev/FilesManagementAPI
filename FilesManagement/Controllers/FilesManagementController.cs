using FilesManagement.Core.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilesManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilesManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesManagementController(IMediator mediator)
        {
            this._mediator = mediator ?? throw new ArgumentNullException();
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DownloadFile(string id)
        {
            var response = await _mediator.Send(new GetFileQuery { FileId = id });
            return Ok(response.FileStream);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            await _mediator.Send(new UploadFileCommand
            {
                File = file
            });
            return Ok();
        }
    }
}
