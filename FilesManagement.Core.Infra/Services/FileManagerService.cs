using FilesManagement.Core.Application.InvertedDependencies;
using Microsoft.AspNetCore.Http;

namespace FilesManagement.Core.Infra.Services
{
    internal class FileManagerService : IFileSystemHelper
    {
        public IFormFile GetFileByPath(string fullSourceFilePath)
        {
            throw new NotImplementedException();
        }

        public void SaveFileToPath(IFormFile file, string fullDestinationFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
