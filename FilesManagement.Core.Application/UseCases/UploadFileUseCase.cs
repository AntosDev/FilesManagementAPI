using FilesManagement.Core.Application.InvertedDependencies;
using FilesManagement.Core.Domain;
using FilesManagement.Core.Domain.InvertedDependencies;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FilesManagement.Core.Application.UseCases
{
    internal class UploadFileUseCase : IRequestHandler<UploadFileCommand>
    {
        IFileSystemHelper filesSystemHelper;
        IFilesRepository filesRepo;
        private readonly IConfiguration configuration;
        public UploadFileUseCase(IFileSystemHelper filesSystemHelper, IFilesRepository filesRepo, IConfiguration configuration)
        {
            this.filesSystemHelper = filesSystemHelper;
            this.filesRepo = filesRepo;
            this.configuration = configuration;
        }

        public Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
           
            var fileInfo = filesSystemHelper.GetInfoFromFile(request.File);
            if (string.IsNullOrEmpty(fileInfo.fileName) || string.IsNullOrEmpty(fileInfo.extension) || fileInfo.size == 0 )
                throw new InvalidDataException("file's name and extension should be specified and the can't be empty");

            var rootPath = configuration["Settings.StorageRootPath"]?.ToString() ?? "~";
            var physicalPath = Path.Combine(rootPath, fileInfo.extension);

            var file = new FileDomain
            {
                Name = fileInfo.fileName,
                Path = physicalPath,
                FileId = Guid.NewGuid().ToString(),
                Size = fileInfo.size,
                CreatedDate = DateTime.UtcNow
            };
            try
            {
                filesSystemHelper.SaveFileToPath(request.File, file);


                filesRepo.Save(new List<FileDomain> { file });
            }catch (Exception ex)
            {
                throw new InvalidOperationException("couldn't save file due to: " + ex.ToString());
            }

            return Unit.Task;
        }
    }
}
