using FilesManagement.Core.Domain;
using Microsoft.AspNetCore.Http;

namespace FilesManagement.Core.Application.InvertedDependencies
{
    public interface IFileSystemHelper
    {
        (string fileName, string extension, long size) GetInfoFromFile(IFormFile file);
        void SaveFileToPath(IFormFile file, FileDomain fileinfo);
        Stream getFileStream(string fullFilePath);

        void DeleteFiles(string fullFilePath);
    }
}
