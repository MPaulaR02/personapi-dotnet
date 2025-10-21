using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

public partial class PersonaDbContext : DbContext
{
    public PersonaDbContext()
    {
    }

    public PersonaDbContext(DbContextOptions<PersonaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<estudio> estudios { get; set; }

    public virtual DbSet<persona> personas { get; set; }

    public virtual DbSet<profesion> profesions { get; set; }

    public virtual DbSet<telefono> telefonos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=persona_db;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<estudio>(entity =>
        {
            entity.HasOne(d => d.cc_perNavigation).WithMany(p => p.estudios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_estudios_persona");

            entity.HasOne(d => d.id_profNavigation).WithMany(p => p.estudios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_estudios_profesion");
        });

        modelBuilder.Entity<persona>(entity =>
        {
            entity.Property(e => e.cc).ValueGeneratedNever();
            entity.Property(e => e.genero).IsFixedLength();
        });

        modelBuilder.Entity<profesion>(entity =>
        {
            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<telefono>(entity =>
        {
            entity.HasOne(d => d.duenioNavigation).WithMany(p => p.telefonos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_telefono_persona");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
