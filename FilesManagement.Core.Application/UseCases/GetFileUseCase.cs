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
        ISqlConnectionFactory _sqlConnectionFactory;

        public GetFileUseCase(IFileSystemHelper filesSystemHelper, IFilesRepository filesRepo, ISqlConnectionFactory _sqlConnectionFactory)
        {
            this.filesSystemHelper = filesSystemHelper;
            this.filesRepo = filesRepo;
            this._sqlConnectionFactory = _sqlConnectionFactory;
        }

        public async Task<GetFileResponse> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var fileDetails = await _sqlConnectionFactory.QuerySingle<FileDomain>(connection, string.Format(request.SQL, request.FileId));
            var file = filesSystemHelper.getFileStream(Path.Combine(fileDetails.Path, fileDetails.Name));
            return new GetFileResponse
            {
                FileStream = file,
                Details = fileDetails
            };
        }
    }
}
