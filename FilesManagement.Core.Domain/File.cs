namespace FilesManagement.Core.Domain
{
    public class FileDomain
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public long Size { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}