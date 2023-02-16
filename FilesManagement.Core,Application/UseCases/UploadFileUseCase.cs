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
            var fileInfo = filesSystemHelper.GetInfoFromFile(request.File);

            var rootPath = "D:";// string.IsNullOrEmpty(_storageSettings.FileStorageLocation) ? "~" : _storageSettings.FileStorageLocation;
            var physicalPath = Path.Combine(rootPath, fileInfo.extension);

            var file = new FileDomain
            {
                Name = fileInfo.fileName,
                Path = physicalPath,
                FileId = Guid.NewGuid().ToString(),
                Size = fileInfo.size,
                CreatedDate = DateTime.UtcNow
            };

            filesSystemHelper.SaveFileToPath(request.File, file);


            filesRepo.Save(new List<FileDomain> { file });

            return Unit.Task;
        }
    }
}
