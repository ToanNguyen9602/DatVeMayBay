using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatVeMayBayApi.Models;

public partial class DatVeMayBayContext : DbContext
{
    public DatVeMayBayContext()
    {
    }

    public DatVeMayBayContext(DbContextOptions<DatVeMayBayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitietchuyenbay> Chitietchuyenbays { get; set; }

    public virtual DbSet<Chuyenbay> Chuyenbays { get; set; }

    public virtual DbSet<Hanhkhach> Hanhkhaches { get; set; }

    public virtual DbSet<Sanbay> Sanbays { get; set; }

    public virtual DbSet<Ve> Ves { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietchuyenbay>(entity =>
        {
            entity.HasKey(e => e.Mact);

            entity.ToTable("chitietchuyenbay");

            entity.Property(e => e.Mact).HasColumnName("mact");
            entity.Property(e => e.Macb).HasColumnName("macb");
            entity.Property(e => e.Masb).HasColumnName("masb");
            entity.Property(e => e.Mota)
                .HasColumnType("text")
                .HasColumnName("mota");
            entity.Property(e => e.Thoigiandung)
                .HasPrecision(0)
                .HasColumnName("thoigiandung");

            entity.HasOne(d => d.MacbNavigation).WithMany(p => p.Chitietchuyenbays)
                .HasForeignKey(d => d.Macb)
                .HasConstraintName("FK_chitietchuyenbay_chuyenbay");

            entity.HasOne(d => d.MasbNavigation).WithMany(p => p.Chitietchuyenbays)
                .HasForeignKey(d => d.Masb)
                .HasConstraintName("FK_chitietchuyenbay_sanbay");
        });

        modelBuilder.Entity<Chuyenbay>(entity =>
        {
            entity.HasKey(e => e.Macb);

            entity.ToTable("chuyenbay");

            entity.Property(e => e.Macb).HasColumnName("macb");
            entity.Property(e => e.Gheloai1).HasColumnName("gheloai1");
            entity.Property(e => e.Gheloai2).HasColumnName("gheloai2");
            entity.Property(e => e.Giagheloai1).HasColumnName("giagheloai1");
            entity.Property(e => e.Giagheloai2).HasColumnName("giagheloai2");
            entity.Property(e => e.Masbden).HasColumnName("masbden");
            entity.Property(e => e.Masbdi).HasColumnName("masbdi");
            entity.Property(e => e.Ngaydi)
                .HasColumnType("date")
                .HasColumnName("ngaydi");
            entity.Property(e => e.Tencb)
                .HasMaxLength(50)
                .HasColumnName("tencb");

            entity.HasOne(d => d.MasbdenNavigation).WithMany(p => p.ChuyenbayMasbdenNavigations)
                .HasForeignKey(d => d.Masbden)
                .HasConstraintName("FK_chuyenbay_sanbay1");

            entity.HasOne(d => d.MasbdiNavigation).WithMany(p => p.ChuyenbayMasbdiNavigations)
                .HasForeignKey(d => d.Masbdi)
                .HasConstraintName("FK_chuyenbay_sanbay");
        });

        modelBuilder.Entity<Hanhkhach>(entity =>
        {
            entity.HasKey(e => e.Mahk);

            entity.ToTable("hanhkhach");

            entity.Property(e => e.Mahk).HasColumnName("mahk");
            entity.Property(e => e.Cmnd).HasColumnName("cmnd");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .HasColumnName("hoten");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("date")
                .HasColumnName("ngaysinh");
        });

        modelBuilder.Entity<Sanbay>(entity =>
        {
            entity.HasKey(e => e.Masanbay);

            entity.ToTable("sanbay");

            entity.Property(e => e.Masanbay).HasColumnName("masanbay");
            entity.Property(e => e.Tensanbay)
                .HasMaxLength(255)
                .HasColumnName("tensanbay");
        });

        modelBuilder.Entity<Ve>(entity =>
        {
            entity.HasKey(e => e.Mave);

            entity.ToTable("ve");

            entity.Property(e => e.Mave).HasColumnName("mave");
            entity.Property(e => e.Giaghe).HasColumnName("giaghe");
            entity.Property(e => e.Loaighe).HasColumnName("loaighe");
            entity.Property(e => e.Macb).HasColumnName("macb");
            entity.Property(e => e.Mahk).HasColumnName("mahk");
            entity.Property(e => e.Soghe).HasColumnName("soghe");

            entity.HasOne(d => d.MacbNavigation).WithMany(p => p.Ves)
                .HasForeignKey(d => d.Macb)
                .HasConstraintName("FK_ve_chuyenbay");

            entity.HasOne(d => d.MahkNavigation).WithMany(p => p.Ves)
                .HasForeignKey(d => d.Mahk)
                .HasConstraintName("FK_ve_hanhkhach");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
