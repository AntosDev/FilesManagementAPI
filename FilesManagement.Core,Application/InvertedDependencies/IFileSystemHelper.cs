using FilesManagement.Core.Domain;
using Microsoft.AspNetCore.Http;

namespace FilesManagement.Core.Application.InvertedDependencies
{
    public interface IFileSystemHelper
    {
        (string fileName, string extension, long size) GetInfoFromFile(IFormFile file);
        void SaveFileToPath(IFormFile file, FileDomain fileinfo);
        IFormFile GetFileByPath(string fullSourceFilePath);
        void DeleteFileFomPath(string fullDestinationFilePath);
    }
}
