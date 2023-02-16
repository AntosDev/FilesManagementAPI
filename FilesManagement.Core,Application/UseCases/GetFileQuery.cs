using FilesManagement.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilesManagement.Core.Application.UseCases
{
    internal class GetFileQuery : IRequest<GetFileResponse>
    {
        public string FileId { get; set; }

        public string SQL
        {
            get
            {
                return "SELECT" +
               $"[file].[file_ID] AS [{nameof(FileDomain.Id)}], " +
               $"[file].[file_Path] AS [{nameof(FileDomain.Path)}], " +
               $"[file].[file_Name] AS [{nameof(FileDomain.Name)}], " +
               $"[file].[file_CreatedDate] AS [{nameof(FileDomain.CreatedDate)}] " +
               $"FROM [dbo].[file]" +
               "WHERE [file].[file_ID] = '{0}'";
            }
        }
    }
    public class GetFileResponse
    {
        public FileDomain Details { get; set; }
        public IFormFile File { get; set; }
    }
}
