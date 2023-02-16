using MediatR;

namespace FilesManagement.Core.Application.UseCases
{
    internal class DeleteFileCommand : IRequest
    {
        public string FileID { get; set; }
    }
}
