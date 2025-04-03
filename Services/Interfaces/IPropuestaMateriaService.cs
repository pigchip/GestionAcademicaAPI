using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de PropuestaMateria
    /// </summary>
    public interface IPropuestaMateriaService
    {
        /// <summary>
        /// Obtiene todas las relaciones PropuestaMateria
        /// </summary>
        /// <returns>IEnumerable de PropuestaMateria</returns>
        Task<IEnumerable<PropuestaMateria>> GetAllAsync();

        /// <summary>
        /// Obtiene una relación PropuestaMateria por su ID
        /// </summary>
        /// <param name="id">Identificador de la relación</param>
        /// <returns>PropuestaMateria encontrada o null</returns>
        Task<PropuestaMateria?> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene relaciones PropuestaMateria por ID de Propuesta
        /// </summary>
        /// <param name="idPropuesta">Identificador de la propuesta</param>
        /// <returns>Relaciones PropuestaMateria de la propuesta</returns>
        Task<IEnumerable<PropuestaMateria>> GetByPropuestaIdAsync(int idPropuesta);

        /// <summary>
        /// Obtiene relaciones PropuestaMateria por ID de Materia
        /// </summary>
        /// <param name="idMateria">Identificador de la materia</param>
        /// <returns>Relaciones PropuestaMateria de la materia</returns>
        Task<IEnumerable<PropuestaMateria>> GetByMateriaIdAsync(int idMateria);

        /// <summary>
        /// Agrega una nueva relación PropuestaMateria
        /// </summary>
        /// <param name="propuestaMateria">Relación a agregar</param>
        /// <returns>Relación PropuestaMateria agregada</returns>
        Task<PropuestaMateria> AddAsync(PropuestaMateria propuestaMateria);

        /// <summary>
        /// Agrega múltiples relaciones PropuestaMateria de una vez
        /// </summary>
        /// <param name="propuestaMaterias">Colección de relaciones a agregar</param>
        /// <returns>Colección de relaciones PropuestaMateria agregadas</returns>
        Task<IEnumerable<PropuestaMateria>> AddRangeAsync(IEnumerable<PropuestaMateria> propuestaMaterias);

        /// <summary>
        /// Elimina una relación PropuestaMateria
        /// </summary>
        /// <param name="id">Identificador de la relación a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Elimina todas las relaciones PropuestaMateria para una propuesta específica
        /// </summary>
        /// <param name="idPropuesta">Identificador de la propuesta</param>
        Task DeleteByPropuestaIdAsync(int idPropuesta);

        /// <summary>
        /// Cuenta el número de materias para una propuesta
        /// </summary>
        /// <param name="idPropuesta">Identificador de la propuesta</param>
        /// <returns>Número de materias en la propuesta</returns>
        Task<int> CountMateriasByPropuestaAsync(int idPropuesta);

        /// <summary>
        /// Cuenta el número de propuestas para una materia
        /// </summary>
        /// <param name="idMateria">Identificador de la materia</param>
        /// <returns>Número de propuestas para la materia</returns>
        Task<int> CountPropuestasByMateriaAsync(int idMateria);
    }
}
