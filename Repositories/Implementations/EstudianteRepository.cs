using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Repositories.Implementations
{
    /// <summary>
    /// Repositorio para gestionar entidades de tipo Estudiante
    /// </summary>
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _IusuarioRepository;
        private readonly ILogger<EstudianteRepository> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EstudianteRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de la base de datos</param>
        /// <param name="configuration">Configuración de la aplicación</param>
        /// <param name="IusuarioRepository">Repositorio de usuario</param>
        /// <param name="logger">Logger para la clase</param>
        public EstudianteRepository(
            AppDbContext context,
            IConfiguration configuration,
            IUsuarioRepository IusuarioRepository,
            ILogger<EstudianteRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _IusuarioRepository = IusuarioRepository ?? throw new ArgumentNullException(nameof(IusuarioRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Agrega un nuevo estudiante
        /// </summary>
        /// <param name="estudiante">Estudiante a agregar</param>
        /// <returns>Estudiante agregado</returns>
        public async Task<Estudiante> AddAsync(Estudiante estudiante)
        {
            if (estudiante == null)
            {
                throw new ArgumentNullException(nameof(estudiante), "El estudiante no puede ser nulo");
            }

            await _context.Set<Estudiante>().AddAsync(estudiante);
            await _context.SaveChangesAsync();
            return estudiante;
        }

        /// <summary>
        /// Obtiene un estudiante por su ID
        /// </summary>
        /// <param name="id">Identificador del estudiante</param>
        /// <returns>Estudiante encontrado o null</returns>
        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            return await _context.Set<Estudiante>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Obtiene un estudiante por su número de boleta
        /// </summary>
        /// <param name="boleta">Número de boleta del estudiante</param>
        /// <returns>Estudiante encontrado o null</returns>
        public async Task<Estudiante?> GetByBoletaAsync(int boleta)
        {
            return await _context.Estudiantes
                .Include(a => a.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Boleta == boleta);
        }

        /// <summary>
        /// Obtiene un estudiante por el ID de usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>Estudiante encontrado o null</returns>
        public async Task<Estudiante?> GetByUserIdAsync(int idUsuario)
        {
            return await _context.Estudiantes
                .Include(e => e.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);
        }

        /// <summary>
        /// Obtiene todos los estudiantes
        /// </summary>
        /// <returns>IEnumerable de estudiantes</returns>
        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Set<Estudiante>()
                .Include(a => a.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Verifica si existe un estudiante con la boleta proporcionada
        /// </summary>
        /// <param name="boleta">Número de boleta</param>
        /// <returns>True si existe, False en caso contrario</returns>
        public async Task<bool> ExistsByBoletaAsync(int boleta)
        {
            return await _context.Set<Estudiante>().AnyAsync(e => e.Boleta == boleta);
        }

        /// <summary>
        /// Verifica si existe un estudiante para un usuario específico
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>True si existe, False en caso contrario</returns>
        public async Task<bool> ExistsForUserAsync(int idUsuario)
        {
            return await _context.Set<Estudiante>().AnyAsync(e => e.IdUsuario == idUsuario);
        }

        /// <summary>
        /// Obtiene estudiantes por carrera
        /// </summary>
        /// <param name="carrera">Nombre de la carrera</param>
        /// <returns>Estudiantes de la carrera especificada</returns>
        public async Task<IEnumerable<Estudiante>> GetByCarreraAsync(string carrera)
        {
            return await _context.Set<Estudiante>()
                .AsNoTracking()
                .Where(e => e.Carrera == carrera)
                .ToListAsync();
        }

        /// <summary>
        /// Actualiza un estudiante existente
        /// </summary>
        /// <param name="estudiante">Estudiante a actualizar</param>
        public async Task<Estudiante> UpdateAsync(Estudiante estudiante)
        {
            if (estudiante == null || estudiante.Usuario == null)
            {
                throw new ArgumentNullException(nameof(estudiante), "El estudiante o su usuario no pueden ser nulos");
            }

            // Attach entity and mark it as modified
            var entry = _context.Entry(estudiante);
            if (entry.State == EntityState.Detached)
            {
                _context.Estudiantes.Attach(estudiante);
            }
            entry.State = EntityState.Modified;

            // Exclude related entity from update to avoid tracking conflicts
            entry.Reference(e => e.Usuario).IsModified = false;

            await _context.SaveChangesAsync();
            return estudiante;
        }

        /// <summary>
        /// Elimina un estudiante
        /// </summary>
        /// <param name="id">Identificador del estudiante a eliminar</param>
        public async Task DeleteAsync(int id)
        {
            var estudiante = await GetByIdAsync(id);

            if (estudiante == null)
            {
                throw new KeyNotFoundException($"Estudiante con ID {id} no encontrado");
            }

            _context.Set<Estudiante>().Remove(estudiante);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Verifica si existe un estudiante con el correo electrónico personal proporcionado
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico personal</param>
        /// <param name="idExcluir">Identificador del estudiante a excluir de la verificación</param>
        /// <returns>True si existe, False en caso contrario</returns>
        public async Task<bool> ExisteCorreoElectronicoPersonalAsync(string correoElectronico, int? idExcluir = null)
        {
            return await _context.Set<Estudiante>()
                .AnyAsync(u => u.EmailEscolar == correoElectronico && (!idExcluir.HasValue || u.Id != idExcluir.Value));
        }

        /// <summary>
        /// Obtiene todos los estudiantes incluyendo todos sus datos relacionados
        /// </summary>
        /// <returns>IEnumerable de estudiantes con todas sus entidades relacionadas</returns>
        public async Task<IEnumerable<Estudiante>> GetAllWithDetailsAsync()
        {
            return await _context.Set<Estudiante>()
                .Include(e => e.Usuario)
                .Include(e => e.Solicitudes)
                    .ThenInclude(s => s.Propuestas)
                        .ThenInclude(p => p.Escuela)
                .Include(e => e.Solicitudes)
                    .ThenInclude(s => s.Propuestas)
                        .ThenInclude(p => p.Materias)
                .Include(e => e.Solicitudes)
                    .ThenInclude(s => s.Comentarios)
                        .ThenInclude(c => c.Usuario)
                .Include(e => e.Materias)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
