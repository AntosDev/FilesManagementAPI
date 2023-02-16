namespace FilesManagement.Core.Domain
{
    public class FileDomain
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}