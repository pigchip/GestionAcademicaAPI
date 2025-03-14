using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Definir las entidades que estarán mapeadas a las tablas de la base de datos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Propuesta> Propuestas { get; set; }
        public DbSet<PropuestaMateria> PropuestaMaterias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones uno a uno
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Administrador)
                .WithOne(a => a.Usuario)
                .HasForeignKey<Administrador>(a => a.IdUsuario);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Estudiante)
                .WithOne(e => e.Usuario)
                .HasForeignKey<Estudiante>(e => e.IdUsuario);

            // Configurar índices únicos
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.EmailPersonal)
                .IsUnique();

            modelBuilder.Entity<Estudiante>()
                .HasIndex(e => e.EmailEscolar)
                .IsUnique();

            modelBuilder.Entity<Estudiante>()
                .HasIndex(e => e.Boleta)
                .IsUnique();

            modelBuilder.Entity<Escuela>()
                .HasIndex(e => e.Nombre)
                .IsUnique();

            // Configurar relaciones uno a muchos
            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Solicitudes)
                .WithOne(s => s.Estudiante)
                .HasForeignKey(s => s.IdEstudiante);

            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Materias)
                .WithOne(m => m.Estudiante)
                .HasForeignKey(m => m.IdEstudiante);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Comentarios)
                .WithOne(c => c.Usuario)
                .HasForeignKey(c => c.IdUsuario);

            modelBuilder.Entity<Materia>()
                .HasMany(m => m.Comentarios)
                .WithOne(c => c.Materia)
                .HasForeignKey(c => c.IdMateria);

            modelBuilder.Entity<Solicitud>()
                .HasMany(s => s.Propuestas)
                .WithOne(p => p.Solicitud)
                .HasForeignKey(p => p.IdSolicitud);

            modelBuilder.Entity<Escuela>()
                .HasMany(e => e.Propuestas)
                .WithOne(p => p.Escuela)
                .HasForeignKey(p => p.IdEscuela);

            // Configurar relaciones de eliminación en cascada para propuesta_materia
            modelBuilder.Entity<PropuestaMateria>()
                .HasOne(pm => pm.Propuesta)
                .WithMany(p => p.PropuestaMaterias)
                .HasForeignKey(pm => pm.IdPropuesta)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PropuestaMateria>()
                .HasOne(pm => pm.Materia)
                .WithMany(m => m.PropuestaMaterias)
                .HasForeignKey(pm => pm.IdMateria)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}