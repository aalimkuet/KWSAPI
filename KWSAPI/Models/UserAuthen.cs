using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KWSAPI.Models
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="varchar(100)")]
        public string? UserName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? Password { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Role { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Phone { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Email { get; set; }
        [Column(TypeName ="varchar(300)")]
        public string? Secretkey { get; set; }
    }
}
