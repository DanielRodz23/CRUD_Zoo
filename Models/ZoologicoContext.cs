using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Zoo.Models;

public partial class ZoologicoContext : DbContext
{
    public ZoologicoContext()
    {
    }

    public ZoologicoContext(DbContextOptions<ZoologicoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animal { get; set; }

    public virtual DbSet<Habitat> Habitat { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;database=Zoologico;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("animal");

            entity.HasIndex(e => e.IdHabitat, "fkHabitatAnimal");

            entity.Property(e => e.NivelPeligroDeExtincion).HasMaxLength(40);
            entity.Property(e => e.Nombre).HasMaxLength(40);
            entity.Property(e => e.TipoAlimentacion).HasMaxLength(40);

            entity.HasOne(d => d.IdHabitatNavigation).WithMany(p => p.Animal)
                .HasForeignKey(d => d.IdHabitat)
                .HasConstraintName("fkHabitatAnimal");
        });

        modelBuilder.Entity<Habitat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("habitat");

            entity.Property(e => e.Nombre).HasMaxLength(40);
            entity.Property(e => e.TipoHabitat).HasMaxLength(20);
            entity.Property(e => e.Vegetacion).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
