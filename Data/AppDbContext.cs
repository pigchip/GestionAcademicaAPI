using GestionAcademicaAPI.Models;
using Microsoft.EntityFrameworkCore;

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
                .HasForeignKey(s => s.IdEstudiante)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada para proteger datos del estudiante

            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Materias)
                .WithOne(m => m.Estudiante)
                .HasForeignKey(m => m.IdEstudiante)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Comentarios)
                .WithOne(c => c.Usuario)
                .HasForeignKey(c => c.IdUsuario);

            modelBuilder.Entity<Solicitud>()
                .HasMany(s => s.Propuestas)
                .WithOne(p => p.Solicitud)
                .HasForeignKey(p => p.IdSolicitud)
                .OnDelete(DeleteBehavior.Cascade); // Si se elimina la solicitud, se eliminan sus propuestas

            modelBuilder.Entity<Solicitud>()
                .HasMany(s => s.Comentarios)
                .WithOne(c => c.Solicitud)
                .HasForeignKey(c => c.IdSolicitud)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Escuela>()
                .HasMany(e => e.Propuestas)
                .WithOne(p => p.Escuela)
                .HasForeignKey(p => p.IdEscuela)
                .OnDelete(DeleteBehavior.Restrict); // No eliminar escuela si tiene propuestas

            // Configurar relación uno a muchos entre Propuesta y Materia
            modelBuilder.Entity<Propuesta>()
                .HasMany(p => p.Materias)
                .WithOne(m => m.Propuesta)
                .HasForeignKey(m => m.IdPropuesta)
                .OnDelete(DeleteBehavior.Cascade); // Si se elimina la propuesta, se eliminan sus materias

            // Configurar relación uno a muchos para RegistroEnvioCorreo
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.RegistroEnvioCorreos)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.IdUsuario);
        }
    }
}