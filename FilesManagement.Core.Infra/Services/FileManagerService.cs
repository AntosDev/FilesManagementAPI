using FilesManagement.Core.Application.InvertedDependencies;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace FilesManagement.Core.Infra.Services
{
    public class FileManagerService : IFileSystemHelper
    {
        public void DeleteFileFomPath(string fullDestinationFilePath)
        {
            throw new NotImplementedException();
        }

        public IFormFile GetFileByPath(string fullSourceFilePath)
        {
            throw new NotImplementedException();
        }

        public void SaveFileToPath(IFormFile file, string fullDestinationFilePath)
        {
            var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);           

            var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
            var comingExtension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1].ToLower();
            Directory.CreateDirectory(fullDestinationFilePath);

            var rootPath = "~/comingExtension"; //string.IsNullOrEmpty(_storageSettings.FileStorageLocation) ? "~" : _storageSettings.FileStorageLocation;
            var physicalPath = Path.Combine(rootPath, fullDestinationFilePath, file.FileName);

            using (var fileStream = new FileStream(fullDestinationFilePath, FileMode.OpenOrCreate))
            {
                file.CopyTo(fileStream);
            }
        }
    }
}
