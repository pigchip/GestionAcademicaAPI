using GestionAcademicaAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Data
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for the Usuario entity.
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Administrador entity.
        /// </summary>
        public DbSet<Administrador> Administradores { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Estudiante entity.
        /// </summary>
        public DbSet<Estudiante> Estudiantes { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Escuela entity.
        /// </summary>
        public DbSet<Escuela> Escuelas { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Solicitud entity.
        /// </summary>
        public DbSet<Solicitud> Solicitudes { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Materia entity.
        /// </summary>
        public DbSet<Materia> Materias { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Propuesta entity.
        /// </summary>
        public DbSet<Propuesta> Propuestas { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the PropuestaMateria entity.
        /// </summary>
        public DbSet<PropuestaMateria> PropuestaMaterias { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Comentario entity.
        /// </summary>
        public DbSet<Comentario> Comentarios { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the RegistroEnvioCorreo entity.
        /// </summary>
        public DbSet<RegistroEnvioCorreo> RegistroEnvioCorreos { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TEntity}"/> properties on the derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
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

            // Configurar relación uno a muchos para RegistroEnvioCorreo
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.RegistroEnvioCorreos)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.IdUsuario);
        }
    }
}