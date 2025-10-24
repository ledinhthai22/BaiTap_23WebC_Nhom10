using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTap_23WebC_Nhom10.Models
{
    [Table("PRODUCTS")]
    public class Product
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(255)]
        [Column("PRODUCT_NAME")]
        public string productName { get; set; } = string.Empty;

        [Range(0, 9999999999999999.99, ErrorMessage = "Giá không hợp lệ")]
        [Column("PRICE", TypeName = "decimal(18,2)")]
        public decimal price { get; set; }

        [Range(0, 100, ErrorMessage = "Giảm giá phải từ 0% đến 100%")]
        [Column("DISCOUNT", TypeName = "decimal(5,2)")]
        public decimal discount { get; set; }

        [StringLength(255)]
        [Column("IMAGE")]
        public string? image { get; set; }

        [StringLength(255)]
        [Column("DESCRIPTION")]
        public string? description { get; set; }

        [Range(0, int.MaxValue)]
        [Column("QUANLITY")]
        public int? quanlity { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Column("CATEGORY_ID")]
        [ForeignKey("Category")]
        public int categoryID { get; set; }

        [Column("TAG_ID")]
        public int? tagID { get; set; }

        [Column("VIEWS")]
        public int views { get; set; } = 0;

        [Column("SELLED")]
        public int selled { get; set; } = 0;

        [Column("STATUS")]
        public bool status { get; set; } = true;

        [Column("CREATE_AT")]
        public DateTime createAT { get; set; } = DateTime.Now;

        [Column("UPDATE_AT")]
        public DateTime? updateAT { get; set; }
        [Column("SLUG")]
        [Required] 
        [MaxLength(255)] 
        public string? slug { get; set; }
        public Category ? category { get; set; }
    }
}
