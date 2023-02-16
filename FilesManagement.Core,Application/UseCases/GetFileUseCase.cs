using FilesManagement.Core.Application.InvertedDependencies;
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

        public Task<GetFileResponse> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var fileDetails = filesRepo.Find(request.FileId);
            var file = filesSystemHelper.GetFileByPath(Path.Combine(fileDetails.Path, fileDetails.Name));
            return Task.FromResult(new GetFileResponse
            {
                File = file,
                Details = fileDetails
            });
        }
    }
}
