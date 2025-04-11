using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.Models;
using GestionAcademicaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace GestionAcademicaAPI.Repositories.Implementations
{
    /// <summary>
    /// Repositorio para gestionar entidades de tipo Administrador
    /// </summary>
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _IusuarioRepository;
        private readonly ILogger<AdministradorRepository> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AdministradorRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de la base de datos</param>
        /// <param name="configuration">Configuración de la aplicación</param>
        /// <param name="IusuarioRepository">Repositorio de usuario</param>
        /// <param name="logger">Logger para la clase</param>
        public AdministradorRepository(
            AppDbContext context,
            IConfiguration configuration,
            IUsuarioRepository IusuarioRepository,
            ILogger<AdministradorRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _IusuarioRepository = IusuarioRepository ?? throw new ArgumentNullException(nameof(IusuarioRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Agrega un nuevo administrador
        /// </summary>
        /// <param name="administrador">Administrador a agregar</param>
        /// <returns>Administrador agregado</returns>
        public async Task<Administrador> AddAsync(Administrador administrador)
        {
            if (administrador == null)
            {
                throw new ArgumentNullException(nameof(administrador), "El administrador no puede ser nulo");
            }

            await _context.Set<Administrador>().AddAsync(administrador);
            await _context.SaveChangesAsync();
            return administrador;
        }

        /// <summary>
        /// Obtiene un administrador por su ID
        /// </summary>
        /// <param name="id">Identificador del administrador</param>
        /// <returns>Administrador encontrado o null</returns>
        public async Task<Administrador?> GetByIdAsync(int id)
        {
            return await _context.Set<Administrador>()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <summary>
        /// Obtiene un administrador por el ID de usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>Administrador encontrado o null</returns>

        public async Task<Administrador?> GetByUserIdAsync(int idUsuario)
        {
            return await _context.Administradores
                .Include(a => a.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdUsuario == idUsuario);
        }

        /// <summary>
        /// Obtiene todos los administradores
        /// </summary>
        /// <returns>IEnumerable de administradores</returns>
        public async Task<IEnumerable<Administrador>> GetAllAsync()
        {
            return await _context.Set<Administrador>()
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Verifica si ya existe un administrador con los mismos datos
        /// </summary>
        /// <param name="administrador">Administrador a verificar</param>
        /// <returns>True si existe un duplicado, False en caso contrario</returns>
        public async Task<bool> IsDuplicateAsync(Administrador administrador)
        {
            if (administrador == null)
            {
                throw new ArgumentNullException(nameof(administrador), "El administrador no puede ser nulo");
            }

            return await _context.Set<Administrador>().AnyAsync(a => a.Usuario.Username == administrador.Usuario.Username || a.Usuario.EmailPersonal == administrador.Usuario.EmailPersonal);
        }

        /// <summary>
        /// Actualiza un administrador existente
        /// </summary>
        /// <param name="administrador">Solicitud de actualización del administrador</param>
        public async Task<Administrador> UpdateAsync(Administrador administrador)
        {
            if (administrador == null || administrador.Usuario == null)
            {
                throw new ArgumentNullException(nameof(administrador), "El Administrador o su usuario no pueden ser nulos");
            }

            // Attach entity and mark it as modified
            var entry = _context.Entry(administrador);
            if (entry.State == EntityState.Detached)
            {
                _context.Administradores.Attach(administrador);
            }
            entry.State = EntityState.Modified;

            // Exclude related entity from update to avoid tracking conflicts
            entry.Reference(e => e.Usuario).IsModified = false;

            await _context.SaveChangesAsync();
            return administrador;
        }

        /// <summary>
        /// Elimina un administrador
        /// </summary>
        /// <param name="id">Identificador del administrador a eliminar</param>
        public async Task DeleteAsync(int id)
        {
            var administrador = await GetByIdAsync(id);

            if (administrador == null)
            {
                throw new KeyNotFoundException($"Administrador con ID {id} no encontrado");
            }

            _context.Set<Administrador>().Remove(administrador);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Busca administradores segúnsu username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Administrador>> FindByUsernameAsync(string username)
        {
            var result = await _context.Set<Administrador>()
                .AsNoTracking()
                .Where(a => a.Usuario.Username == username)
                .ToListAsync();

            return result ?? new List<Administrador>();
        }

        /// <summary>
        /// Verifica si existe un administrador para un usuario específico
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>True si existe, False en caso contrario</returns>
        public async Task<bool> ExistsForUserAsync(int idUsuario)
        {
            return await _context.Set<Administrador>().AnyAsync(a => a.IdUsuario == idUsuario);
        }
    }
}
