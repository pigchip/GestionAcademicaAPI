using GestionAcademicaAPI.Models.Entities;
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
    }
}
