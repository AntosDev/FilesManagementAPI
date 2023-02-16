using FilesManagement.Core.Application.InvertedDependencies;
using FilesManagement.Core.Domain.InvertedDependencies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesManagement.Core.Application.UseCases
{
    internal class DeleteFileUseCase : IRequestHandler<DeleteFileCommand>
    {
        IFileSystemHelper filesSystemHelper;
        IFilesRepository filesRepo;

        public DeleteFileUseCase(IFileSystemHelper filesSystemHelper, IFilesRepository filesRepo)
        {
            this.filesSystemHelper = filesSystemHelper;
            this.filesRepo = filesRepo;
        }
        public Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = filesRepo.Find(request.FileID);
            filesSystemHelper.DeleteFiles(Path.Combine(file.Path, file.Name));           

            filesRepo.Delete(new List<string> { file.FileId });
            return Unit.Task;
        }
    }
}
