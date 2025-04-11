using GestionAcademicaAPI.Dtos;
using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAcademicaAPI.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de Estudiantes
    /// </summary>
    public interface IEstudianteService
    {
        /// <summary>
        /// Agrega un nuevo estudiante
        /// </summary>
        /// <param name="estudiante">Estudiante a agregar</param>
        /// <returns>Estudiante agregado</returns>
        Task<EstudianteDTO> AddAsync(EstudianteDTO estudiante);

        /// <summary>
        /// Obtener estudiante con validación de usuario
        /// </summary>
        Task<Estudiante> GetByCredentials(string username, string password);

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
        /// Obtiene estudiantes por carrera
        /// </summary>
        /// <param name="carrera">Nombre de la carrera</param>
        /// <returns>Estudiantes de la carrera especificada</returns>
        Task<IEnumerable<Estudiante>> GetByCarreraAsync(string carrera);

        /// <summary>
        /// Obtiene todos los estudiantes
        /// </summary>
        /// <returns>IEnumerable de estudiantes</returns>
        Task<IEnumerable<Estudiante>> GetAllAsync();

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
        /// Actualiza un estudiante existente
        /// </summary>
        /// <param name="estudiante">Estudiante a actualizar</param>
        Task<EstudianteDTO> UpdateAsync(EstudianteDTO estudiante);

        /// <summary>
        /// Elimina un estudiante
        /// </summary>
        /// <param name="id">Identificador del estudiante a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Verifica si un correo electrónico personal ya está en uso.
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico a verificar.</param>
        /// <param name="id">ID del estudiante (opcional, para verificar la unicidad en actualizaciones).</param>
        /// <returns>True si el correo electrónico ya existe; de lo contrario, false.</returns>
        Task<bool> ExisteCorreoElectronicoPersonalAsync(string correoElectronico, int? id = null);

        /// <summary>
        /// Obtiene todos los estudiantes con sus datos relacionados (Usuario, Solicitudes, Materias)
        /// </summary>
        /// <returns>IEnumerable de estudiantes con todas sus entidades relacionadas</returns>
        Task<IEnumerable<EstudianteDetalladoDTO>> GetAllDetailAsync();
    }
}
