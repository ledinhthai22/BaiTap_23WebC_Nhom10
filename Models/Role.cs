using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTap_23WebC_Nhom10.Models
{
    [Table("ROLE")]
    public class Role
    {
        [Key] // Đây là khóa chính
        [Column("ID")]
        public int Id { get; set; }

        [Column("ROLE_NAME")]
        [StringLength(255)]
        public string RoleName { get; set; } = string.Empty;
        public ICollection<User>? Users { get; set; }

    }
}
