using MediatR;

namespace FilesManagement.Core.Application.UseCases
{
    public class DeleteFileCommand : IRequest
    {
        public string FileID { get; set; }
    }
}
