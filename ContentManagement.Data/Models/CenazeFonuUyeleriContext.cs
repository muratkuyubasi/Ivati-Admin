using System;
using System.Collections.Generic;
using ContentManagement.Data.Models;
using IsvecExcelAktarma.Models;
using Microsoft.EntityFrameworkCore;

namespace ContentManagement.Data.Models;

public partial class CenazeFonuUyeleriContext : DbContext
{
    public CenazeFonuUyeleriContext()
    {
    }

    public CenazeFonuUyeleriContext(DbContextOptions<CenazeFonuUyeleriContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CenazeFonuUyeleri> CenazeFonuUyeleris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=CenazeFonuUyeleri;Trusted_Connection=true; TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CenazeFonuUyeleri>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CenazeFonuUyeleri");

            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.Anm).HasMaxLength(550);
            entity.Property(e => e.CreationDate).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstKid).HasMaxLength(150);
            entity.Property(e => e.FirstKidDateOfBirth).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(150);
            entity.Property(e => e.Föd3)
                .HasMaxLength(150)
                .HasColumnName("föd3");
            entity.Property(e => e.Föd4)
                .HasMaxLength(150)
                .HasColumnName("föd4");
            entity.Property(e => e.Föd5)
                .HasMaxLength(150)
                .HasColumnName("föd5");
            entity.Property(e => e.Föd6)
                .HasMaxLength(150)
                .HasColumnName("föd6");
            entity.Property(e => e.Föd7)
                .HasMaxLength(150)
                .HasColumnName("föd7");
            entity.Property(e => e.Föd8)
                .HasMaxLength(150)
                .HasColumnName("föd8");
            entity.Property(e => e.IdentificationNumber).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(150);
            entity.Property(e => e.Namn3).HasMaxLength(150);
            entity.Property(e => e.Namn4).HasMaxLength(150);
            entity.Property(e => e.Namn5).HasMaxLength(150);
            entity.Property(e => e.Namn6).HasMaxLength(150);
            entity.Property(e => e.Namn7).HasMaxLength(150);
            entity.Property(e => e.Namn8).HasMaxLength(150);
            entity.Property(e => e.NotlarNotAciklama)
                .HasMaxLength(150)
                .HasColumnName("NOTLAR_not_aciklama");
            entity.Property(e => e.NotlarNotTarihi)
                .HasColumnType("date")
                .HasColumnName("NOTLAR_not_tarihi");
            entity.Property(e => e.NotlarNotTuru)
                .HasMaxLength(150)
                .HasColumnName("NOTLAR_not_turu");
            entity.Property(e => e.Ort).HasMaxLength(150);
            entity.Property(e => e.PhoneNumber).HasMaxLength(150);
            entity.Property(e => e.PhoneNumber2).HasMaxLength(150);
            entity.Property(e => e.PostCode).HasMaxLength(150);
            entity.Property(e => e.SecondKid).HasMaxLength(150);
            entity.Property(e => e.SecondKidDateOfBirth).HasMaxLength(150);
            entity.Property(e => e.Spouse).HasMaxLength(150);
            entity.Property(e => e.SpouseIdentificationNumber).HasMaxLength(150);
            entity.Property(e => e.Ucret)
                .HasColumnType("money")
                .HasColumnName("ucret");
            entity.Property(e => e._2024).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
