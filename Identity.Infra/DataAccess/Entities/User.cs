using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Infra.DataAccess.Entities
{
    [Table("fm_User")]
    public class UserEntity
    {

        public UserEntity()
        {
            // For entity framework
        }

        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long ID { get; set; }

        [Column("usr_ID")]
        [Required]
        public string UserId { get; set; }

        [Column("usr_UserName")]
        [Required]
        public string Username { get; set; }

        [Column("usr_FirstName")]
        [Required]
        public string FirstName { get; set; }

        [Column("usr_LastName")]
        [Required]
        public string LastName { get; set; }

        [Column("usr_Password")]
        [Required]
        public string Password { get; set; }
    }
}
