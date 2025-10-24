using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTap_23WebC_Nhom10.Models
{
    [Table("TAGS")]
    public class Tag
    {
        [Column("ID")]
        [Key]
        public int Id { get; set; }

        [Column("TAG_NAME")]
        public string TagName { get; set; } = string.Empty;
    }
}
