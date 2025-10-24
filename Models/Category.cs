using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTap_23WebC_Nhom10.Models
{
    [Table("CATEGORY")]
    public class Category
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("CATEGORY_NAME")]
        public string? CategoryName { get; set; }
        [Column("STATUS")]
        public bool Status { get; set; } = true;

        public Product? Products { get; set; }
    }
}
