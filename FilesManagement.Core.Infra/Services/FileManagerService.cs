using FilesManagement.Core.Application.InvertedDependencies;
using FilesManagement.Core.Domain;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace FilesManagement.Core.Infra.Services
{
    public class FileManagerService : IFileSystemHelper
    {

        public (string fileName, string extension, long size) GetInfoFromFile(IFormFile file)
        {
            var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
            var comingExtension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1].ToLower();


            return (fileName, comingExtension, file.Length);
        }

        public void SaveFileToPath(IFormFile file, FileDomain fileinfo)
        {
            Directory.CreateDirectory(fileinfo.Path);
            using (var fileStream = new FileStream(Path.Combine(fileinfo.Path, fileinfo.Name), FileMode.OpenOrCreate))
            {
                file.CopyTo(fileStream);
            }
        }
        public Stream getFileStream(string fullFilePath)
        {
            return new FileStream(fullFilePath, FileMode.Open);
        }

        public void DeleteFiles(string fullFilePath)
        {
            try
            {
                File.Delete(fullFilePath);
            }
            catch (Exception) { }
        }
    }
}
