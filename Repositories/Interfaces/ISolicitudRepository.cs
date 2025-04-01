using GestionAcademicaAPI.Models;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Repositories.Interfaces
{
    /// <summary>
    /// Interface para el repositorio de solicitudes
    /// </summary>
    public interface ISolicitudRepository
    {
        /// <summary>
        /// Obtiene todas las solicitudes
        /// </summary>
        /// <returns>IEnumerable de solicitudes</returns>
        Task<IEnumerable<Solicitud>> GetAllAsync();

        /// <summary>
        /// Obtiene una solicitud por su ID
        /// </summary>
        /// <param name="id">Identificador de la solicitud</param>
        /// <returns>Solicitud encontrada o null</returns>
        Task<Solicitud?> GetByIdAsync(int id);

        /// <summary>
        /// Busca solicitudes según un criterio específico
        /// </summary>
        /// <param name="predicate">Condición de búsqueda</param>
        /// <returns>Solicitudes que cumplen el criterio</returns>
        Task<IEnumerable<Solicitud>> FindAsync(Expression<Func<Solicitud, bool>> predicate);

        /// <summary>
        /// Obtiene solicitudes por ID de estudiante
        /// </summary>
        /// <param name="idEstudiante">Identificador del estudiante</param>
        /// <returns>Solicitudes del estudiante</returns>
        Task<IEnumerable<Solicitud>> GetByEstudianteIdAsync(int idEstudiante);

        /// <summary>
        /// Obtiene solicitudes por status
        /// </summary>
        /// <param name="status">Status de la solicitud</param>
        /// <returns>Solicitudes con el status especificado</returns>
        Task<IEnumerable<Solicitud>> GetByStatusAsync(string status);

        /// <summary>
        /// Obtiene solicitudes dentro de un rango de fechas
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio</param>
        /// <param name="fechaFin">Fecha de fin</param>
        /// <returns>Solicitudes en el rango de fechas</returns>
        Task<IEnumerable<Solicitud>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin);

        /// <summary>
        /// Agrega una nueva solicitud
        /// </summary>
        /// <param name="solicitud">Solicitud a agregar</param>
        /// <returns>Solicitud agregada</returns>
        Task<Solicitud> AddAsync(Solicitud solicitud);

        /// <summary>
        /// Actualiza una solicitud existente
        /// </summary>
        /// <param name="solicitud">Solicitud a actualizar</param>
        Task UpdateAsync(Solicitud solicitud);

        /// <summary>
        /// Elimina una solicitud
        /// </summary>
        /// <param name="id">Identificador de la solicitud a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Cuenta el número de solicitudes por estudiante
        /// </summary>
        /// <param name="idEstudiante">Identificador del estudiante</param>
        /// <returns>Número de solicitudes del estudiante</returns>
        Task<int> CountByEstudianteAsync(int idEstudiante);

        /// <summary>
        /// Cuenta el número de solicitudes por status
        /// </summary>
        /// <param name="status">Status de la solicitud</param>
        /// <returns>Número de solicitudes con el status especificado</returns>
        Task<int> CountByStatusAsync(string status);

        /// <summary>
        /// Obtiene la última solicitud de un estudiante
        /// </summary>
        /// <param name="idEstudiante">Identificador del estudiante</param>
        /// <returns>Última solicitud del estudiante o null</returns>
        Task<Solicitud?> GetLastSolicitudByEstudianteAsync(int idEstudiante);
    }
}