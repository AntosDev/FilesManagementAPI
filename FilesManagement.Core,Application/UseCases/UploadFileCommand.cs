using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilesManagement.Core.Application.UseCases
{
    internal class UploadFileCommand : IRequest
    {
        public IFormFile file { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }
}
