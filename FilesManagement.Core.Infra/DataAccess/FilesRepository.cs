using FilesManagement.Core.Domain.InvertedDependencies;

namespace FilesManagement.Core.Infra.DataAccess
{
    internal class FilesRepository : IFilesRepository
    {
        public void Save(IEnumerable<Domain.File> files)
        {
            throw new NotImplementedException();
        }
        public void Delete(IEnumerable<Domain.File> files)
        {
            throw new NotImplementedException();
        }
    }
}
