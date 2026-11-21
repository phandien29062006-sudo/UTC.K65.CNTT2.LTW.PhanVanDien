using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PhanVanDien_241230675.Models;

public partial class QLTaiSanContext : DbContext
{
    public QLTaiSanContext()
    {
    }

    public QLTaiSanContext(DbContextOptions<QLTaiSanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LoaiTaiSan> LoaiTaiSans { get; set; }

    public virtual DbSet<TaiSan> TaiSans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=QLTaiSan;User Id=sa;Password=0606;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaiSan>(entity =>
        {
            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.TaiSans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiSan_LoaiTaiSan");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
