using FilesManagement.Core.Domain;
using MediatR;

namespace FilesManagement.Core.Application.UseCases
{
    public class GetFilesQuery : IRequest<IEnumerable<FileDomain>>
    {
        public string SQL
        {
            get
            {
                return "SELECT" +
               $"[fm_File].[file_ID] AS [{nameof(FileDomain.FileId)}], " +
               $"[fm_File].[file_Path] AS [{nameof(FileDomain.Path)}], " +
               $"[fm_File].[file_Name] AS [{nameof(FileDomain.Name)}], " +
               $"[fm_File].[file_CreatedDate] AS [{nameof(FileDomain.CreatedDate)}] " +
               $"FROM [dbo].[fm_File]";
            }
        }
    }
}
