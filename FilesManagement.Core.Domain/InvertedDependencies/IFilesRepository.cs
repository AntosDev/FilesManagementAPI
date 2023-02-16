namespace FilesManagement.Core.Domain.InvertedDependencies
{
    public interface IFilesRepository
    {
        void Save(IEnumerable<FileDomain> files);
        void Delete(IEnumerable<FileDomain> files);
    }
}
