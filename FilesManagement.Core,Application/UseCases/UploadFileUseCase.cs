using FilesManagement.Core.Application.InvertedDependencies;
using FilesManagement.Core.Domain;
using FilesManagement.Core.Domain.InvertedDependencies;
using MediatR;

namespace FilesManagement.Core.Application.UseCases
{
    internal class UploadFileUseCase : IRequestHandler<UploadFileCommand>
    {
        IFileSystemHelper filesSystemHelper;
        IFilesRepository filesRepo;

        public UploadFileUseCase(IFileSystemHelper filesSystemHelper, IFilesRepository filesRepo)
        {
            this.filesSystemHelper = filesSystemHelper;
            this.filesRepo = filesRepo;
        }

        public Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            filesSystemHelper.SaveFileToPath(request.file, request.Path);
            var file = new FileDomain
            { 
                Name = request.FileName, 
                CreatedDate = DateTime.UtcNow 
            };

            filesRepo.Save(new List<FileDomain> { file });
            return Unit.Task;
        }
    }
}
