﻿// <auto-generated />
using System;
using Fundacion.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fundacion.Migrations
{
    [DbContext(typeof(FundacionContext))]
    partial class FundacionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fundacion.Models.Asistencia", b =>
                {
                    b.Property<int>("AsiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("asiId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AsiId"));

                    b.Property<double>("AsCantHsRedondeo")
                        .HasColumnType("float")
                        .HasColumnName("asCantHsRedondeo");

                    b.Property<DateTime>("AsEgreso")
                        .HasColumnType("datetime")
                        .HasColumnName("asEgreso");

                    b.Property<DateTime>("AsIngreso")
                        .HasColumnType("datetime")
                        .HasColumnName("asIngreso");

                    b.Property<bool>("AsPresent")
                        .HasColumnType("bit")
                        .HasColumnName("asPresent");

                    b.Property<int>("EsId")
                        .HasColumnType("int")
                        .HasColumnName("esId");

                    b.HasKey("AsiId");

                    b.HasIndex("EsId");

                    b.ToTable("Asistencias");
                });

            modelBuilder.Entity("Fundacion.Models.Aula", b =>
                {
                    b.Property<int>("AuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("auId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuId"));

                    b.Property<string>("AuDescripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("auDescripcion");

                    b.HasKey("AuId");

                    b.ToTable("Aulas");
                });

            modelBuilder.Entity("Fundacion.Models.Categoria", b =>
                {
                    b.Property<int>("CaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("caId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CaId"));

                    b.Property<string>("CaDescripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("caDescripcion");

                    b.Property<double>("CaValorHora")
                        .HasColumnType("float")
                        .HasColumnName("caValorHora");

                    b.HasKey("CaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Fundacion.Models.Espacio", b =>
                {
                    b.Property<int>("EsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EsId"));

                    b.Property<int>("CaId")
                        .HasColumnType("int")
                        .HasColumnName("caId");

                    b.Property<bool>("EsActivo")
                        .HasColumnType("bit")
                        .HasColumnName("esActivo");

                    b.Property<string>("EsDescripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("esDescripcion");

                    b.Property<int>("TuId")
                        .HasColumnType("int")
                        .HasColumnName("tuId");

                    b.Property<int>("UsId")
                        .HasColumnType("int")
                        .HasColumnName("usId");

                    b.HasKey("EsId");

                    b.HasIndex("CaId");

                    b.HasIndex("TuId");

                    b.HasIndex("UsId");

                    b.ToTable("Espacios");
                });

            modelBuilder.Entity("Fundacion.Models.Localidad", b =>
                {
                    b.Property<int>("LcId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("lcId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LcId"));

                    b.Property<string>("LcDescripcion")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("lcDescripcion");

                    b.Property<int>("PvId")
                        .HasColumnType("int")
                        .HasColumnName("pvId");

                    b.HasKey("LcId");

                    b.HasIndex("PvId");

                    b.ToTable("Localidades");
                });

            modelBuilder.Entity("Fundacion.Models.Provincia", b =>
                {
                    b.Property<int>("PvId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pvId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PvId"));

                    b.Property<string>("PvDescripcion")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("pvDescripcion");

                    b.HasKey("PvId");

                    b.ToTable("Provincias");
                });

            modelBuilder.Entity("Fundacion.Models.Role", b =>
                {
                    b.Property<int>("RoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("roId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoId"));

                    b.Property<string>("RoDenominacion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("roDenominacion");

                    b.HasKey("RoId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Fundacion.Models.Subespacio", b =>
                {
                    b.Property<int>("SeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("seId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeId"));

                    b.Property<int>("AuId")
                        .HasColumnType("int")
                        .HasColumnName("auId");

                    b.Property<int>("EsId")
                        .HasColumnType("int")
                        .HasColumnName("esId");

                    b.Property<double>("SeCantHs")
                        .HasColumnType("float")
                        .HasColumnName("seCantHs");

                    b.Property<string>("SeDia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("seDia");

                    b.Property<TimeSpan?>("SeHora")
                        .HasMaxLength(10)
                        .HasColumnType("time")
                        .HasColumnName("seHora");

                    b.HasKey("SeId");

                    b.HasIndex("AuId");

                    b.HasIndex("EsId");

                    b.ToTable("Subespacios");
                });

            modelBuilder.Entity("Fundacion.Models.Turno", b =>
                {
                    b.Property<int>("TuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("tuId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TuId"));

                    b.Property<string>("TuDescripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("tuDescripcion");

                    b.HasKey("TuId")
                        .HasName("PK_Trunos");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("Fundacion.Models.Usuario", b =>
                {
                    b.Property<int>("UsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("usId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsId"));

                    b.Property<int>("RoId")
                        .HasColumnType("int")
                        .HasColumnName("roId");

                    b.Property<bool>("UsActivo")
                        .HasColumnType("bit")
                        .HasColumnName("usActivo");

                    b.Property<string>("UsApellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("usApellido");

                    b.Property<string>("UsContrasena")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("usContrasena");

                    b.Property<string>("UsDireccion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("usDireccion");

                    b.Property<long>("UsDni")
                        .HasColumnType("bigint")
                        .HasColumnName("usDNI");

                    b.Property<string>("UsEmail")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("usEmail");

                    b.Property<int>("UsLocalidad")
                        .HasColumnType("int")
                        .HasColumnName("usLocalidad");

                    b.Property<string>("UsNombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("usNombre");

                    b.Property<int>("UsProvincia")
                        .HasColumnType("int")
                        .HasColumnName("usProvincia");

                    b.Property<long?>("UsTelefono")
                        .HasColumnType("bigint")
                        .HasColumnName("usTelefono");

                    b.Property<DateTime>("date_created")
                        .HasColumnType("datetime")
                        .HasColumnName("date_created");

                    b.Property<string>("token_recovery")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("token_recovery");

                    b.HasKey("UsId");

                    b.HasIndex("RoId");

                    b.HasIndex("UsLocalidad");

                    b.HasIndex("UsProvincia");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Fundacion.Models.Asistencia", b =>
                {
                    b.HasOne("Fundacion.Models.Espacio", "Es")
                        .WithMany("Asistencia")
                        .HasForeignKey("EsId")
                        .IsRequired()
                        .HasConstraintName("FK_Asistencias_Espacios");

                    b.Navigation("Es");
                });

            modelBuilder.Entity("Fundacion.Models.Espacio", b =>
                {
                    b.HasOne("Fundacion.Models.Categoria", "Ca")
                        .WithMany("Espacios")
                        .HasForeignKey("CaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Espacios_Categorias");

                    b.HasOne("Fundacion.Models.Turno", "Tu")
                        .WithMany("Espacios")
                        .HasForeignKey("TuId")
                        .IsRequired()
                        .HasConstraintName("FK_Espacios_Turnos");

                    b.HasOne("Fundacion.Models.Usuario", "Us")
                        .WithMany("Espacios")
                        .HasForeignKey("UsId")
                        .IsRequired()
                        .HasConstraintName("FK_Espacios_Usuarios");

                    b.Navigation("Ca");

                    b.Navigation("Tu");

                    b.Navigation("Us");
                });

            modelBuilder.Entity("Fundacion.Models.Localidad", b =>
                {
                    b.HasOne("Fundacion.Models.Provincia", "Pv")
                        .WithMany("Localidades")
                        .HasForeignKey("PvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Localidades_Provincias");

                    b.Navigation("Pv");
                });

            modelBuilder.Entity("Fundacion.Models.Subespacio", b =>
                {
                    b.HasOne("Fundacion.Models.Aula", "Au")
                        .WithMany("Subespacios")
                        .HasForeignKey("AuId")
                        .IsRequired()
                        .HasConstraintName("FK_Subespacios_Aulas");

                    b.HasOne("Fundacion.Models.Espacio", "Es")
                        .WithMany("Subespacios")
                        .HasForeignKey("EsId")
                        .IsRequired()
                        .HasConstraintName("FK_Subespacios_Espacios");

                    b.Navigation("Au");

                    b.Navigation("Es");
                });

            modelBuilder.Entity("Fundacion.Models.Usuario", b =>
                {
                    b.HasOne("Fundacion.Models.Role", "Ro")
                        .WithMany("Usuarios")
                        .HasForeignKey("RoId")
                        .IsRequired()
                        .HasConstraintName("FK_Usuarios_Roles");

                    b.HasOne("Fundacion.Models.Localidad", "Lc")
                        .WithMany("Usuarios")
                        .HasForeignKey("UsLocalidad")
                        .IsRequired()
                        .HasConstraintName("FK_Usuarios_Localidades");

                    b.HasOne("Fundacion.Models.Provincia", "Pv")
                        .WithMany("Usuarios")
                        .HasForeignKey("UsProvincia")
                        .IsRequired()
                        .HasConstraintName("FK_Usuarios_Provincias");

                    b.Navigation("Lc");

                    b.Navigation("Pv");

                    b.Navigation("Ro");
                });

            modelBuilder.Entity("Fundacion.Models.Aula", b =>
                {
                    b.Navigation("Subespacios");
                });

            modelBuilder.Entity("Fundacion.Models.Categoria", b =>
                {
                    b.Navigation("Espacios");
                });

            modelBuilder.Entity("Fundacion.Models.Espacio", b =>
                {
                    b.Navigation("Asistencia");

                    b.Navigation("Subespacios");
                });

            modelBuilder.Entity("Fundacion.Models.Localidad", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Fundacion.Models.Provincia", b =>
                {
                    b.Navigation("Localidades");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Fundacion.Models.Role", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Fundacion.Models.Turno", b =>
                {
                    b.Navigation("Espacios");
                });

            modelBuilder.Entity("Fundacion.Models.Usuario", b =>
                {
                    b.Navigation("Espacios");
                });
#pragma warning restore 612, 618
        }
    }
}
