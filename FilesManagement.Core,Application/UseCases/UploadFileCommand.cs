using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilesManagement.Core.Application.UseCases
{
    public class UploadFileCommand : IRequest
    {
        public IFormFile File { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }
}
