
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLA7.Areas.Admins.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(220)]
        [Column(TypeName = "varchar(220)")]
        public string? Slug { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? Image { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public byte Status { get; set; } = 1;

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public Category? Category { get; set; }
    }
}
