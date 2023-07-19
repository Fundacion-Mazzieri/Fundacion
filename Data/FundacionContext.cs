using System;
using System.Collections.Generic;
using Fundacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Fundacion.Data;

public partial class FundacionContext : DbContext
{
    public FundacionContext()
    {
    }

    public FundacionContext(DbContextOptions<FundacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }

    public virtual DbSet<Aula> Aulas { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Espacio> Espacios { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.AsiId);

            entity.Property(e => e.AsiId).HasColumnName("asiId");
            entity.Property(e => e.AsEgreso)
                .HasColumnType("datetime")
                .HasColumnName("asEgreso");
            entity.Property(e => e.AsIngreso)
                .HasColumnType("datetime")
                .HasColumnName("asIngreso");
            entity.Property(e => e.AsPresent).HasColumnName("asPresent");
            entity.Property(e => e.EsId).HasColumnName("esId");

            entity.HasOne(d => d.Es).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.EsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asistencias_Espacios");
        });

        modelBuilder.Entity<Aula>(entity =>
        {
            entity.HasKey(e => e.AuId);

            entity.Property(e => e.AuId).HasColumnName("auId");
            entity.Property(e => e.AuDescripcion)
                .HasMaxLength(50)
                .HasColumnName("auDescripcion");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CaId);

            entity.Property(e => e.CaId).HasColumnName("caId");
            entity.Property(e => e.CaDescripcion)
                .HasMaxLength(50)
                .HasColumnName("caDescripcion");
            entity.Property(e => e.CaValorHora).HasColumnName("caValorHora");
        });

        modelBuilder.Entity<Espacio>(entity =>
        {
            entity.HasKey(e => e.EsId);

            entity.Property(e => e.EsId)
                .ValueGeneratedNever()
                .HasColumnName("esId");
            entity.Property(e => e.AuId).HasColumnName("auId");
            entity.Property(e => e.CaId).HasColumnName("caId");
            entity.Property(e => e.EsActivo)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("esActivo");
            entity.Property(e => e.EsCantHs).HasColumnName("esCantHs");
            entity.Property(e => e.EsDescripcion)
                .HasMaxLength(50)
                .HasColumnName("esDescripcion");
            entity.Property(e => e.EsDia)
                .HasMaxLength(10)
                .HasColumnName("esDia");
            entity.Property(e => e.EsHora)
                .HasMaxLength(10)
                .HasColumnName("esHora");
            entity.Property(e => e.TuId).HasColumnName("tuId");
            entity.Property(e => e.UsId).HasColumnName("usId");

            entity.HasOne(d => d.Au).WithMany(p => p.Espacios)
                .HasForeignKey(d => d.AuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Espacios_Aulas");

            entity.HasOne(d => d.Ca).WithMany(p => p.Espacios)
                .HasForeignKey(d => d.CaId)
                .HasConstraintName("FK_Espacios_Categorias");

            entity.HasOne(d => d.Tu).WithMany(p => p.Espacios)
                .HasForeignKey(d => d.TuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Espacios_Turnos");

            entity.HasOne(d => d.Us).WithMany(p => p.Espacios)
                .HasForeignKey(d => d.UsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Espacios_Usuarios");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoId);

            entity.Property(e => e.RoId).HasColumnName("roId");
            entity.Property(e => e.RoDenominacion)
                .HasMaxLength(50)
                .HasColumnName("roDenominacion");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.TuId).HasName("PK_Trunos");

            entity.Property(e => e.TuId).HasColumnName("tuId");
            entity.Property(e => e.TuDescripcion)
                .HasMaxLength(50)
                .HasColumnName("tuDescripcion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsId);

            entity.Property(e => e.UsId).HasColumnName("usId");
            entity.Property(e => e.RoId).HasColumnName("roId");
            entity.Property(e => e.UsActivo).HasColumnName("usActivo");
            entity.Property(e => e.UsApellido)
                .HasMaxLength(50)
                .HasColumnName("usApellido");
            entity.Property(e => e.UsContrasena)
                .HasMaxLength(50)
                .HasColumnName("usContrasena");
            entity.Property(e => e.UsDireccion)
                .HasMaxLength(50)
                .HasColumnName("usDireccion");
            entity.Property(e => e.UsDni).HasColumnName("usDNI");
            entity.Property(e => e.UsEmail)
                .HasMaxLength(50)
                .HasColumnName("usEmail");
            entity.Property(e => e.UsLocalidad)
                .HasMaxLength(50)
                .HasColumnName("usLocalidad");
            entity.Property(e => e.UsNombre)
                .HasMaxLength(50)
                .HasColumnName("usNombre");
            entity.Property(e => e.UsProvincia)
                .HasMaxLength(50)
                .HasColumnName("usProvincia");
            entity.Property(e => e.UsTelefono).HasColumnName("usTelefono");

            entity.HasOne(d => d.Ro).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
