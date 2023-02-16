using FilesManagement.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilesManagement.Core.Application.UseCases
{
    public class GetFileQuery : IRequest<GetFileResponse>
    {
        public string FileId { get; set; }

        public string SQL
        {
            get
            {
                return "SELECT" +
               $"[fm_File].[file_ID] AS [{nameof(FileDomain.FileId)}], " +
               $"[fm_File].[file_Path] AS [{nameof(FileDomain.Path)}], " +
               $"[fm_File].[file_Name] AS [{nameof(FileDomain.Name)}], " +
               $"[fm_File].[file_CreatedDate] AS [{nameof(FileDomain.CreatedDate)}] " +
               $"FROM [dbo].[fm_File]" +
               "WHERE [fm_File].[file_ID] = '{0}'";
            }
        }
    }
    public class GetFileResponse
    {
        public FileDomain Details { get; set; }
        public Stream FileStream { get; set; }
    }
}
