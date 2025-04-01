using GestionAcademicaAPI.Models;
using System.Linq.Expressions;

namespace GestionAcademicaAPI.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de Materias
    /// </summary>
    public interface IMateriaRepository
    {
        /// <summary>
        /// Obtiene todas las materias
        /// </summary>
        /// <returns>IEnumerable de materias</returns>
        Task<IEnumerable<Materia>> GetAllAsync();

        /// <summary>
        /// Obtiene una materia por su ID
        /// </summary>
        /// <param name="id">Identificador de la materia</param>
        /// <returns>Materia encontrada o null</returns>
        Task<Materia?> GetByIdAsync(int id);

        /// <summary>
        /// Busca materias según un criterio específico
        /// </summary>
        /// <param name="predicate">Condición de búsqueda</param>
        /// <returns>Materias que cumplen el criterio</returns>
        Task<IEnumerable<Materia>> FindAsync(Expression<Func<Materia, bool>> predicate);

        /// <summary>
        /// Obtiene materias por ID de estudiante
        /// </summary>
        /// <param name="idEstudiante">Identificador del estudiante</param>
        /// <returns>Materias del estudiante</returns>
        Task<IEnumerable<Materia>> GetByEstudianteIdAsync(int idEstudiante);

        /// <summary>
        /// Obtiene materias por nombre de materia ESCOM
        /// </summary>
        /// <param name="nombreMateriaEscom">Nombre de la materia ESCOM</param>
        /// <returns>Materias con el nombre especificado</returns>
        Task<IEnumerable<Materia>> GetByNombreMateriaEscomAsync(string nombreMateriaEscom);

        /// <summary>
        /// Obtiene materias por nombre de materia foránea
        /// </summary>
        /// <param name="nombreMateriaForanea">Nombre de la materia foránea</param>
        /// <returns>Materias con el nombre especificado</returns>
        Task<IEnumerable<Materia>> GetByNombreMateriaForaneaAsync(string nombreMateriaForanea);

        /// <summary>
        /// Obtiene materias por status
        /// </summary>
        /// <param name="status">Status de la materia</param>
        /// <returns>Materias con el status especificado</returns>
        Task<IEnumerable<Materia>> GetByStatusAsync(string status);

        /// <summary>
        /// Agrega una nueva materia
        /// </summary>
        /// <param name="materia">Materia a agregar</param>
        /// <returns>Materia agregada</returns>
        Task<Materia> AddAsync(Materia materia);

        /// <summary>
        /// Actualiza una materia existente
        /// </summary>
        /// <param name="materia">Materia a actualizar</param>
        Task UpdateAsync(Materia materia);

        /// <summary>
        /// Elimina una materia
        /// </summary>
        /// <param name="id">Identificador de la materia a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Cuenta el número de materias por estudiante
        /// </summary>
        /// <param name="idEstudiante">Identificador del estudiante</param>
        /// <returns>Número de materias del estudiante</returns>
        Task<int> CountByEstudianteAsync(int idEstudiante);

        /// <summary>
        /// Cuenta el número de materias por status
        /// </summary>
        /// <param name="status">Status de la materia</param>
        /// <returns>Número de materias con el status especificado</returns>
        Task<int> CountByStatusAsync(string status);

        /// <summary>
        /// Verifica si existe una materia con el nombre de materia ESCOM
        /// </summary>
        /// <param name="nombreMateriaEscom">Nombre de la materia ESCOM</param>
        /// <returns>True si existe, False en caso contrario</returns>
        Task<bool> ExistsByNombreMateriaEscomAsync(string nombreMateriaEscom);

        /// <summary>
        /// Verifica si existe una materia con el nombre de materia foránea
        /// </summary>
        /// <param name="nombreMateriaForanea">Nombre de la materia foránea</param>
        /// <returns>True si existe, False en caso contrario</returns>
        Task<bool> ExistsByNombreMateriaForaneaAsync(string nombreMateriaForanea);
    }
}