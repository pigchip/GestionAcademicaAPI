using MyProject.Models;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Data.Interfaces
{
    public interface IComentarioRepository
    {
        /// <summary>
        /// Obtiene todos los comentarios
        /// </summary>
        /// <returns>IEnumerable de comentarios</returns>
        Task<IEnumerable<Comentario>> GetAllAsync();

        /// <summary>
        /// Obtiene un comentario por su ID
        /// </summary>
        /// <param name="id">Identificador del comentario</param>
        /// <returns>Comentario encontrado o null</returns>
        Task<Comentario?> GetByIdAsync(int id);

        /// <summary>
        /// Busca comentarios según un criterio específico
        /// </summary>
        /// <param name="predicate">Condición de búsqueda</param>
        /// <returns>Comentarios que cumplen el criterio</returns>
        Task<IEnumerable<Comentario>> FindAsync(Expression<Func<Comentario, bool>> predicate);

        /// <summary>
        /// Obtiene comentarios por ID de usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>Comentarios del usuario</returns>
        Task<IEnumerable<Comentario>> GetByUserIdAsync(int idUsuario);

        /// <summary>
        /// Obtiene comentarios por ID de materia
        /// </summary>
        /// <param name="idMateria">Identificador de la materia</param>
        /// <returns>Comentarios de la materia</returns>
        Task<IEnumerable<Comentario>> GetByMateriaIdAsync(int idMateria);

        /// <summary>
        /// Obtiene comentarios dentro de un rango de fechas
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio</param>
        /// <param name="fechaFin">Fecha de fin</param>
        /// <returns>Comentarios en el rango de fechas</returns>
        Task<IEnumerable<Comentario>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin);

        /// <summary>
        /// Agrega un nuevo comentario
        /// </summary>
        /// <param name="comentario">Comentario a agregar</param>
        /// <returns>Comentario agregado</returns>
        Task<Comentario> AddAsync(Comentario comentario);

        /// <summary>
        /// Actualiza un comentario existente
        /// </summary>
        /// <param name="comentario">Comentario a actualizar</param>
        Task UpdateAsync(Comentario comentario);

        /// <summary>
        /// Elimina un comentario
        /// </summary>
        /// <param name="id">Identificador del comentario a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Cuenta el número de comentarios para una materia específica
        /// </summary>
        /// <param name="idMateria">Identificador de la materia</param>
        /// <returns>Número de comentarios en la materia</returns>
        Task<int> CountByMateriaAsync(int idMateria);

        /// <summary>
        /// Cuenta el número de comentarios realizados por un usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>Número de comentarios del usuario</returns>
        Task<int> CountByUserAsync(int idUsuario);
    }
}