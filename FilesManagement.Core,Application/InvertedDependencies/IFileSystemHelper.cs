using Microsoft.AspNetCore.Http;

namespace FilesManagement.Core.Application.InvertedDependencies
{
    public interface IFileSystemHelper
    {
        void SaveFileToPath(IFormFile file, string fullDestinationFilePath);
        IFormFile GetFileByPath(string fullSourceFilePath);
    }
}
