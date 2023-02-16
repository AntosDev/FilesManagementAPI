using FilesManagement.Core.Domain;
using FilesManagement.Core.Domain.InvertedDependencies;

namespace FilesManagement.Core.Infra.DataAccess
{
    internal class FilesRepository : IFilesRepository
    {
        public void Save(IEnumerable<Domain.FileDomain> files)
        {
            throw new NotImplementedException();
        }
        public FileDomain Find(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}