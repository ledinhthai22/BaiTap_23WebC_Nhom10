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
        public int id { get; set; }

        [Column("NAME")]
        [StringLength(255)]
        [Required(ErrorMessage = "Tên là bắt buộc")]
        public string? name { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [StringLength(255)]
        [Column("USER_NAME")]
        public string userName { get; set; } = string.Empty;

        [Column("PASSWORD")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(255)]
        public string passWord { get; set; } = string.Empty;

        [Column("ROLE_ID")]
        [ForeignKey("Role")]
        public int roleID { get; set; }

        [Column("EMAIL")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(255)]
        
        public string? email { get; set; }
        
        [Column("Phone")]
        [StringLength(32)]
        public string? phone { get; set; }

        [Column("STATUS")]
        public bool status { get; set; } = true;

        [Column("CREATE_AT")]
        public DateTime? createAt { get; set; } = DateTime.Now;

        [Column("UPDATE_AT")]
        public DateTime? updateAt { get; set; }
    }
}
