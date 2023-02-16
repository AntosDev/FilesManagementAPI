using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilesManagement.Core.Infra.DataAccess.Entities
{
    [Table("fm_File")]
    public class FileEntity
    {
        public FileEntity()
        {
            // For entity framework
        }
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Column("file_ID")]
        [Required]
        public string FileId { get; set; }

        [Column("file_Name")]
        [Required]
        public string Name { get; set; }

        [Column("file_Path")]
        [Required]
        public string Path { get; set; }

        [Column("file_Size")]
        [Required]
        public long Size { get; set; }

        [Column("file_CreatedDate")]
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
