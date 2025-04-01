using GestionAcademicaAPI.Models;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de Estudiantes
    /// </summary>
    public interface IEstudianteRepository
    {
        /// <summary>
        /// Obtiene todos los estudiantes
        /// </summary>
        /// <returns>IEnumerable de estudiantes</returns>
        Task<IEnumerable<Estudiante>> GetAllAsync();

        /// <summary>
        /// Obtiene un estudiante por su ID
        /// </summary>
        /// <param name="id">Identificador del estudiante</param>
        /// <returns>Estudiante encontrado o null</returns>
        Task<Estudiante?> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene un estudiante por su número de boleta
        /// </summary>
        /// <param name="boleta">Número de boleta del estudiante</param>
        /// <returns>Estudiante encontrado o null</returns>
        Task<Estudiante?> GetByBoletaAsync(int boleta);

        /// <summary>
        /// Obtiene un estudiante por el ID de usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>Estudiante encontrado o null</returns>
        Task<Estudiante?> GetByUserIdAsync(int idUsuario);

        /// <summary>
        /// Busca estudiantes según un criterio específico
        /// </summary>
        /// <param name="predicate">Condición de búsqueda</param>
        /// <returns>Estudiantes que cumplen el criterio</returns>
        Task<IEnumerable<Estudiante>> FindAsync(Expression<Func<Estudiante, bool>> predicate);

        /// <summary>
        /// Agrega un nuevo estudiante
        /// </summary>
        /// <param name="estudiante">Estudiante a agregar</param>
        /// <returns>Estudiante agregado</returns>
        Task<Estudiante> AddAsync(Estudiante estudiante);

        /// <summary>
        /// Actualiza un estudiante existente
        /// </summary>
        /// <param name="estudiante">Estudiante a actualizar</param>
        Task UpdateAsync(Estudiante estudiante);

        /// <summary>
        /// Elimina un estudiante
        /// </summary>
        /// <param name="id">Identificador del estudiante a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Verifica si existe un estudiante con la boleta proporcionada
        /// </summary>
        /// <param name="boleta">Número de boleta</param>
        /// <returns>True si existe, False en caso contrario</returns>
        Task<bool> ExistsByBoletaAsync(int boleta);

        /// <summary>
        /// Verifica si existe un estudiante para un usuario específico
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>True si existe, False en caso contrario</returns>
        Task<bool> ExistsForUserAsync(int idUsuario);

        /// <summary>
        /// Obtiene estudiantes por carrera
        /// </summary>
        /// <param name="carrera">Nombre de la carrera</param>
        /// <returns>Estudiantes de la carrera especificada</returns>
        Task<IEnumerable<Estudiante>> GetByCarreraAsync(string carrera);
    }
}