using FilesManagement.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilesManagement.Core.Application.UseCases
{
    internal class GetFileQuery : IRequest<GetFileResponse>
    {
        public string FileId { get; set; }
    }
    internal class GetFileResponse
    {
        public FileDomain Details { get; set; }
        public IFormFile File { get; set; }
    }
}
