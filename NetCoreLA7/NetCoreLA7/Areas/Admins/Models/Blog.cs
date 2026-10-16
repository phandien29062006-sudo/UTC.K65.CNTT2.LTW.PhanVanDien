
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLA7.Areas.Admins.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề bài viết")]
        [StringLength(250)]
        public string Title { get; set; } = string.Empty;

        [StringLength(270)]
        [Column(TypeName = "varchar(270)")]
        public string? Slug { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? Image { get; set; }

        [StringLength(1000)]
        public string? Summary { get; set; }

        public string? Content { get; set; }

        [StringLength(100)]
        public string? Author { get; set; }

        public byte Status { get; set; } = 1;

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? PublishedDate { get; set; }

        public Category? Category { get; set; }
    }
}
