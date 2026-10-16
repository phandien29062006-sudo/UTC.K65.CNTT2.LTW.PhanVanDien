
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLA7.Areas.Admins.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề banner")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên file ảnh")]
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Image { get; set; } = string.Empty;

        [StringLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string? Link { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int SortOrder { get; set; }

        public byte Status { get; set; } = 1;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
