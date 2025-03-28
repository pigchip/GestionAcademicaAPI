using MyProject.Models;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Data.Interfaces
{
    public interface IAdministradorRepository
    {
        /// <summary>
        /// Obtiene todos los administradores
        /// </summary>
        /// <returns>IEnumerable de administradores</returns>
        Task<IEnumerable<Administrador>> GetAllAsync();

        /// <summary>
        /// Obtiene un administrador por su ID
        /// </summary>
        /// <param name="id">Identificador del administrador</param>
        /// <returns>Administrador encontrado o null</returns>
        Task<Administrador?> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene un administrador por el ID de usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>Administrador encontrado o null</returns>
        Task<Administrador?> GetByUserIdAsync(int idUsuario);

        /// <summary>
        /// Busca administradores según un criterio específico
        /// </summary>
        /// <param name="predicate">Condición de búsqueda</param>
        /// <returns>Administradores que cumplen el criterio</returns>
        Task<IEnumerable<Administrador>> FindAsync(Expression<Func<Administrador, bool>> predicate);

        /// <summary>
        /// Agrega un nuevo administrador
        /// </summary>
        /// <param name="administrador">Administrador a agregar</param>
        /// <returns>Administrador agregado</returns>
        Task<Administrador> AddAsync(Administrador administrador);

        /// <summary>
        /// Actualiza un administrador existente
        /// </summary>
        /// <param name="administrador">Administrador a actualizar</param>
        Task UpdateAsync(Administrador administrador);

        /// <summary>
        /// Elimina un administrador
        /// </summary>
        /// <param name="id">Identificador del administrador a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Verifica si existe un administrador para un usuario específico
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>True si existe, False en caso contrario</returns>
        Task<bool> ExistsForUserAsync(int idUsuario);
    }
}