using FilesManagement.Core.Domain.InvertedDependencies;

namespace FilesManagement.Core.Infra.DataAccess
{
    internal class FilesRepository : IFilesRepository
    {
        public async void Save(IEnumerable<Domain.FileDomain> files)
        {
            throw new NotImplementedException();
        }
        public void Delete(IEnumerable<Domain.FileDomain> files)
        {
            throw new NotImplementedException();
        }
    }
}