using GestionAcademicaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Services.Interfaces
{
    /// <summary>
    /// Interface para el servicio de propuestas
    /// </summary>
    public interface IPropuestaService
    {
        /// <summary>
        /// Agrega una nueva propuesta
        /// </summary>
        /// <param name="propuesta">Propuesta a agregar</param>
        /// <returns>Propuesta agregada</returns>
        Task<Propuesta> AddAsync(Propuesta propuesta);

        /// <summary>
        /// Obtiene todas las propuestas
        /// </summary>
        /// <returns>IEnumerable de propuestas</returns>
        Task<IEnumerable<Propuesta>> GetAllAsync();

        /// <summary>
        /// Obtiene una propuesta por su ID
        /// </summary>
        /// <param name="id">Identificador de la propuesta</param>
        /// <returns>Propuesta encontrada o null</returns>
        Task<Propuesta?> GetByIdAsync(int id);

        /// <summary>
        /// Busca propuestas según un criterio específico
        /// </summary>
        /// <param name="predicate">Condición de búsqueda</param>
        /// <returns>Propuestas que cumplen el criterio</returns>
        Task<IEnumerable<Propuesta>> FindAsync(Expression<Func<Propuesta, bool>> predicate);

        /// <summary>
        /// Obtiene propuestas por ID de solicitud
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <returns>Propuestas de la solicitud</returns>
        Task<IEnumerable<Propuesta>> GetBySolicitudIdAsync(int idSolicitud);

        /// <summary>
        /// Obtiene propuestas por ID de escuela
        /// </summary>
        /// <param name="idEscuela">Identificador de la escuela</param>
        /// <returns>Propuestas de la escuela</returns>
        Task<IEnumerable<Propuesta>> GetByEscuelaIdAsync(int idEscuela);

        /// <summary>
        /// Obtiene propuestas por status
        /// </summary>
        /// <param name="status">Status de la propuesta</param>
        /// <returns>Propuestas con el status especificado</returns>
        Task<IEnumerable<Propuesta>> GetByStatusAsync(string status);

        /// <summary>
        /// Obtiene propuestas dentro de un rango de fechas
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio</param>
        /// <param name="fechaFin">Fecha de fin</param>
        /// <returns>Propuestas en el rango de fechas</returns>
        Task<IEnumerable<Propuesta>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin);

        /// <summary>
        /// Actualiza una propuesta existente
        /// </summary>
        /// <param name="propuesta">Propuesta a actualizar</param>
        Task UpdateAsync(Propuesta propuesta);

        /// <summary>
        /// Elimina una propuesta
        /// </summary>
        /// <param name="id">Identificador de la propuesta a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Cuenta el número de propuestas por solicitud
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <returns>Número de propuestas de la solicitud</returns>
        Task<int> CountBySolicitudAsync(int idSolicitud);

        /// <summary>
        /// Cuenta el número de propuestas por escuela
        /// </summary>
        /// <param name="idEscuela">Identificador de la escuela</param>
        /// <returns>Número de propuestas de la escuela</returns>
        Task<int> CountByEscuelaAsync(int idEscuela);

        /// <summary>
        /// Cuenta el número de propuestas por status
        /// </summary>
        /// <param name="status">Status de la propuesta</param>
        /// <returns>Número de propuestas con el status especificado</returns>
        Task<int> CountByStatusAsync(string status);

        /// <summary>
        /// Obtiene la última propuesta de una solicitud
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <returns>Última propuesta de la solicitud o null</returns>
        Task<Propuesta?> GetLastPropuestaBySolicitudAsync(int idSolicitud);
    }
}
