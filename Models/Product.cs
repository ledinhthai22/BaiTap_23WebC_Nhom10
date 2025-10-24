using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaiTap_23WebC_Nhom10.Migrations;

namespace BaiTap_23WebC_Nhom10.Models
{
    [Table("PRODUCTS")]
    public class Product
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("PRODUCT_NAME")]
        public string? ProductName { get; set; }

        [Column("PRICE")]
        public decimal Price { get; set; }

        [Column("QUANLITY")]
        public int Quanlity { get; set; }

        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        [Column("DISCOUNT")]
        public decimal? Discount { get; set; }

        [Column("CATEGORY_ID")]
        [ForeignKey("CATEGORY")]
        public int? CategoryId { get; set; }

        [Column("TAG_ID")]
        [ForeignKey("TAGS")]
        public int? TagId { get; set; }

        [Column("VIEWS")]
        public int Views { get; set; } = 0;

        [Column("SELLED")]
        public int Selled { get; set; } = 0;

        [Column("STATUS")]
        public bool Status { get; set; } = true;

        [Column("SLUG")]
        public string? Slug { get; set; }

        [Column("CREATE_AT")]
        public DateTime CreateAt { get; set; } = DateTime.Now;

        [Column("UPDATE_AT")]
        public DateTime? UpdateAt { get; set; }

        // Navigation
        public Category? Category { get; set; }
        public Tag? Tag { get; set; }
        //public ICollection<Comment> Comments { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        //public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}