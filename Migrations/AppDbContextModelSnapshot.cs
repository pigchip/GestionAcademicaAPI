﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyProject.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestionAcademicaAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GestionAcademicaAPI.Models.Administrador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario")
                        .IsUnique();

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdSolicitud")
                        .HasColumnType("integer");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdSolicitud");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Escuela", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Escuelas");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Estudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApellidoMat")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ApellidoPat")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Boleta")
                        .HasColumnType("integer");

                    b.Property<string>("Carrera")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("EmailEscolar")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer");

                    b.Property<string>("InePdf")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Boleta")
                        .IsUnique();

                    b.HasIndex("EmailEscolar")
                        .IsUnique();

                    b.HasIndex("IdUsuario")
                        .IsUnique();

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Materia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdEstudiante")
                        .HasColumnType("integer");

                    b.Property<int>("IdPropuesta")
                        .HasColumnType("integer");

                    b.Property<string>("NombreMateriaEscom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("NombreMateriaForanea")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("TemarioMateriaForaneaUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdEstudiante");

                    b.HasIndex("IdPropuesta");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Propuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdEscuela")
                        .HasColumnType("integer");

                    b.Property<int>("IdSolicitud")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("IdEscuela");

                    b.HasIndex("IdSolicitud");

                    b.ToTable("Propuestas");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.RegistroEnvioCorreo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DetallesError")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaEnvio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer");

                    b.Property<bool>("Resultado")
                        .HasColumnType("boolean");

                    b.Property<string>("TipoCorreo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("RegistroEnvioCorreos");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Solicitud", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdEstudiante")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("IdEstudiante");

                    b.ToTable("Solicitudes");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailPersonal")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ResetPasswordToken")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("ResetPasswordTokenExpiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.HasIndex("EmailPersonal")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Administrador", b =>
                {
                    b.HasOne("GestionAcademicaAPI.Models.Usuario", "Usuario")
                        .WithOne("Administrador")
                        .HasForeignKey("GestionAcademicaAPI.Models.Administrador", "IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Comentario", b =>
                {
                    b.HasOne("GestionAcademicaAPI.Models.Solicitud", "Solicitud")
                        .WithMany("Comentarios")
                        .HasForeignKey("IdSolicitud")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionAcademicaAPI.Models.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Solicitud");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Estudiante", b =>
                {
                    b.HasOne("GestionAcademicaAPI.Models.Usuario", "Usuario")
                        .WithOne("Estudiante")
                        .HasForeignKey("GestionAcademicaAPI.Models.Estudiante", "IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Materia", b =>
                {
                    b.HasOne("GestionAcademicaAPI.Models.Estudiante", "Estudiante")
                        .WithMany("Materias")
                        .HasForeignKey("IdEstudiante")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GestionAcademicaAPI.Models.Propuesta", "Propuesta")
                        .WithMany("Materias")
                        .HasForeignKey("IdPropuesta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Propuesta");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Propuesta", b =>
                {
                    b.HasOne("GestionAcademicaAPI.Models.Escuela", "Escuela")
                        .WithMany("Propuestas")
                        .HasForeignKey("IdEscuela")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GestionAcademicaAPI.Models.Solicitud", "Solicitud")
                        .WithMany("Propuestas")
                        .HasForeignKey("IdSolicitud")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escuela");

                    b.Navigation("Solicitud");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.RegistroEnvioCorreo", b =>
                {
                    b.HasOne("GestionAcademicaAPI.Models.Usuario", "Usuario")
                        .WithMany("RegistroEnvioCorreos")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Solicitud", b =>
                {
                    b.HasOne("GestionAcademicaAPI.Models.Estudiante", "Estudiante")
                        .WithMany("Solicitudes")
                        .HasForeignKey("IdEstudiante")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Escuela", b =>
                {
                    b.Navigation("Propuestas");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Estudiante", b =>
                {
                    b.Navigation("Materias");

                    b.Navigation("Solicitudes");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Propuesta", b =>
                {
                    b.Navigation("Materias");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Solicitud", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Propuestas");
                });

            modelBuilder.Entity("GestionAcademicaAPI.Models.Usuario", b =>
                {
                    b.Navigation("Administrador");

                    b.Navigation("Comentarios");

                    b.Navigation("Estudiante");

                    b.Navigation("RegistroEnvioCorreos");
                });
#pragma warning restore 612, 618
        }
    }
}
