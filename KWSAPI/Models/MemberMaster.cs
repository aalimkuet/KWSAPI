using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KWS.Models
{
    public class MemberMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Column(TypeName="nvarchar(100)")]
        public string MemberName { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Roll { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Batch { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string PassingYear { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string Cgpa { get; set; }
    }
}
