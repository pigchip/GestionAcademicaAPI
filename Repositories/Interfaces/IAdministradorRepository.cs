using GestionAcademicaAPI.Helpers;
using GestionAcademicaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionAcademicaAPI.Repositories.Interfaces
{

    /// <summary>
    /// Interfaz para el repositorio de administradores
    /// </summary>
    public interface IAdministradorRepository
    {

        /// <summary>
        /// Agrega un nuevo administrador
        /// </summary>
        /// <param name="administrador">Administrador a agregar</param>
        /// <returns>Administrador agregado</returns>
        Task<Administrador> AddAsync(Administrador administrador);

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
        /// Obtiene todos los administradores
        /// </summary>
        /// <returns>IEnumerable de administradores</returns>
        Task<IEnumerable<Administrador>> GetAllAsync();

        /// <summary>
        /// Actualiza un administrador existente
        /// </summary>
        /// <param name="administrador">Administrador a actualizar</param>
        Task<Administrador> UpdateAsync(ActualizarAdminRequest administrador);

        /// <summary>
        /// Encontrar un administrador por su nombre de usuario 
        /// </summary>
        Task<IEnumerable<Administrador>> FindByUsernameAsync(string username);

        /// <summary>
        /// Elimina un administrador
        /// </summary>
        /// <param name="id">Identificador del administrador a eliminar</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Verifica si ya existe un administrador con los mismos datos
        /// </summary>
        /// <param name="administrador">Administrador a verificar</param>
        /// <returns>True si existe un duplicado, False en caso contrario</returns>
        Task<bool> IsDuplicateAsync(Administrador administrador);

        /// <summary>
        /// Verifica si existe un administrador para un usuario específico
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>True si existe, False en caso contrario</returns>
        Task<bool> ExistsForUserAsync(int idUsuario);
    }
}