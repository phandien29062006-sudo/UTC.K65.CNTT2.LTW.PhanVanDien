using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PhanVanDien_241230675.Models;

[Table("TheLoai")]
public partial class TheLoai
{
    [Key]
    [Column("MaTL")]
    public int MaTl { get; set; }

    [Column("TenTL")]
    [StringLength(50)]
    public string TenTl { get; set; } = null!;

    [InverseProperty("MaTlNavigation")]
    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
