namespace FilesManagement.Core.Domain.InvertedDependencies
{
    public interface IFilesRepository
    {
        FileDomain Find(string id);
        void Save(IEnumerable<FileDomain> files);
        void Delete(IEnumerable<string> ids);
    }
}
