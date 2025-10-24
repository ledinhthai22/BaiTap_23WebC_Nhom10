using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTap_23WebC_Nhom10.Models
{
    [Table("PRODUCT_IMAGES")]
    public class ProductImage
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("PRODUCT_ID")]
        public int ProductId { get; set; }
        [Column("IMAGE_PATH")]
        public string? ImagePath { get; set; }
        [Column("IS_MAIN")]
        public bool IsMain { get; set; } = false;

        // Navigation
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
