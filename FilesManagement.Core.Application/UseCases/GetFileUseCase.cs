using FilesManagement.Common.Application.InvertedDependencies;
using FilesManagement.Core.Application.InvertedDependencies;
using FilesManagement.Core.Domain;
using FilesManagement.Core.Domain.InvertedDependencies;
using MediatR;

namespace FilesManagement.Core.Application.UseCases
{
    internal class GetFileUseCase : IRequestHandler<GetFileQuery, GetFileResponse>
    {
        IFileSystemHelper filesSystemHelper;
        IFilesRepository filesRepo;
        
        public GetFileUseCase(IFileSystemHelper filesSystemHelper, IFilesRepository filesRepo)
        {
            this.filesSystemHelper = filesSystemHelper;
            this.filesRepo = filesRepo;
        }

        public async Task<GetFileResponse> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var fileDetails = this.filesRepo.Find(request.FileId);

            if (fileDetails == null) throw new InvalidDataException("File With Specified ID Not found");

            var file = filesSystemHelper.getFileStream(Path.Combine(fileDetails.Path, fileDetails.Name));
            return new GetFileResponse
            {
                FileStream = file,
                Details = fileDetails
            };
        }
    }
}
