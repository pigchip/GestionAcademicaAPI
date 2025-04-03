using GestionAcademicaAPI.Models;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de Escuelas
    /// </summary>
    public interface IEscuelaService
    {
        /// <summary>
        /// Obtiene todas las escuelas
        /// </summary>
        /// <returns>IEnumerable de escuelas</returns>
        Task<IEnumerable<Escuela>> GetAllAsync();

        /// <summary>
        /// Obtiene una escuela por su ID
        /// </summary>
        /// <param name="id">Identificador de la escuela</param>
        /// <returns>Escuela encontrada o null</returns>
        Task<Escuela?> GetByIdAsync(int id);

        /// <summary>
        /// Busca escuelas según un criterio específico
        /// </summary>
        /// <param name="predicate">Condición de búsqueda</param>
        /// <returns>Escuelas que cumplen el criterio</returns>
        Task<IEnumerable<Escuela>> FindAsync(Expression<Func<Escuela, bool>> predicate);

        /// <summary>
        /// Obtiene una escuela por su nombre
        /// </summary>
        /// <param name="nombre">Nombre de la escuela</param>
        /// <returns>Escuela encontrada o null</returns>
        Task<Escuela?> GetByNombreAsync(string nombre);

        /// <summary>
        /// Agrega una nueva escuela
        /// </summary>
        /// <param name="escuela">Escuela a agregar</param>
        /// <returns>Escuela agregada</returns>
        Task<Escuela> AddAsync(Escuela escuela);

        /// <summary>
        /// Actualiza una escuela existente
        /// </summary>
        /// <param name="escuela">Escuela a actualizar</param>
        Task UpdateAsync(Escuela escuela);

        /// <summary>
        /// Elimina una escuela
        /// </summary>
        /// <param name="id">Identificador de la escuela a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Verifica si existe una escuela con el nombre proporcionado
        /// </summary>
        /// <param name="nombre">Nombre de la escuela</param>
        /// <returns>True si existe, False en caso contrario</returns>
        Task<bool> ExistsByNombreAsync(string nombre);

        /// <summary>
        /// Obtiene el número total de escuelas
        /// </summary>
        /// <returns>Número de escuelas</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Obtiene escuelas que han realizado propuestas
        /// </summary>
        /// <returns>Escuelas con propuestas</returns>
        Task<IEnumerable<Escuela>> GetEscuelasWithPropostasAsync();
    }
}
