using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MojeFilmyAPI.Models;

public partial class MojeFilmyContext : DbContext
{
    

    public MojeFilmyContext(DbContextOptions<MojeFilmyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Filmy> Filmies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filmy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Filmy__3214EC07E7DDAE8A");

            entity.ToTable("Filmy");

            entity.Property(e => e.Director)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Year).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
