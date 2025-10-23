using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTap_23WebC_Nhom10.Models
{
    [Table("TAGS")]
    public class Tag
    {
        [Column("ID")]
        [Key]
        public int id { get; set; }

        [Column("TAG_NAME")]
        [Required(ErrorMessage = "Tên tag là bắt buộc")]
        [StringLength(255, ErrorMessage = "Tên tag tối đa 255 ký tự")]
        public string tagName { get; set; } = string.Empty;
    }
}
