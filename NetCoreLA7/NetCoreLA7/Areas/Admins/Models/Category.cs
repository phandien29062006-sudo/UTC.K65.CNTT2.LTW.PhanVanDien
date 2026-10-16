
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLA7.Areas.Admins.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
        public string Name { get; set; } = string.Empty;

        public byte Status { get; set; } = 1;

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.Today;

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Image { get; set; }

        [StringLength(350)]
        public string? Description { get; set; }
    }
}
