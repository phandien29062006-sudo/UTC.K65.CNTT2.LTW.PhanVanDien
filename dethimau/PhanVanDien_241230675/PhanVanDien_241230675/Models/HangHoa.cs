using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PhanVanDien_241230675.Models;

[Table("HangHoa")]
public partial class HangHoa
{
    [Key]
    public int MaHang { get; set; }

    public int MaLoai { get; set; }

    [StringLength(50)]
    public string TenHang { get; set; } = null!;

    //[Column(TypeName = "numeric(18, 0)")]
    [Range(typeof(decimal),"100","5000",
        ErrorMessage ="Giá phải từ 100- 5000")]
    public decimal? Gia { get; set; }

    //[StringLength(100)]
    [RegularExpression(@"(?i)^.*\.(jpg|png|gif|tiff)$",
        ErrorMessage ="Ảnh phải có đuôi jpg,png,gif,tiff")]
    public string? Anh { get; set; }

    [ForeignKey("MaLoai")]
    [InverseProperty("HangHoas")]
    public virtual LoaiHang MaLoaiNavigation { get; set; } = null!;
}
