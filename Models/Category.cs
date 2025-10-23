using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTap_23WebC_Nhom10.Models
{
    [Table("CATEGORY")]
    public class Category
    {
        
        [Key]
        [Column("ID")]
        public int id { get; set; }
        
        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        [StringLength(255, ErrorMessage = "Tên danh mục tối đa 255 ký tự")]
        [Column("CATEGORY_NAME")]
        public string categoryName { get; set; } = string.Empty;
        [Column("STATUS")]
        [Required]
        public bool status { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
