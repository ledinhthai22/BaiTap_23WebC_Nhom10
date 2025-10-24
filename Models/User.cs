using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BaiTap_23WebC_Nhom10.Models
{
    [Table("USER")]
    public class User
    {
        [Column("ID")]
        [Key]
        public int Id { get; set; }

        [Column("NAME")]
        [StringLength(255)]
        [Required(ErrorMessage = "Tên là bắt buộc")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [StringLength(255)]
        [Column("USER_NAME")]
        public string UserName { get; set; } = string.Empty;

        [Column("PASSWORD")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(255)]
        public string PassWord { get; set; } = string.Empty;

        [Column("ROLE_ID")]
        [ForeignKey("ROLE")]
        public int RoleID { get; set; }

        [Column("EMAIL")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(255)]

        public string? Email { get; set; }

        [Column("PHONE")]
        [StringLength(32)]
        public string? Phone { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; } = true;

        [Column("CREATE_AT")]
        public DateTime? CreateAt { get; set; } = DateTime.Now;

        [Column("UPDATE_AT")]
        public DateTime? UpdateAt { get; set; }
    }
}
