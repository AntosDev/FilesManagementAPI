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
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetFiles()
        {
            var response = await _mediator.Send(new GetFilesQuery());
            if(response == null || response.Count() == 0) return NoContent();
            return Ok(response);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DownloadFile(string id)
        {
            try
            {
                var response = await _mediator.Send(new GetFileQuery { FileId = id });
                return Ok(response.FileStream);
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
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
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteFile(string id)
        {
            var response = await _mediator.Send(new DeleteFileCommand { FileID = id });
            return NoContent();
        }
    }
}
