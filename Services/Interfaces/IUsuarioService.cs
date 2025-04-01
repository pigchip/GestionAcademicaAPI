using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de gestión de usuarios.
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario a registrar.</param>
        /// <returns>El usuario registrado.</returns>
        Task<Usuario> RegistrarUsuarioAsync(UsuarioDTO usuarioDTO);

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>El usuario si existe; de lo contrario, null.</returns>
        Task<Usuario?> ObtenerUsuarioPorIdAsync(int id);

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario.</param>
        /// <returns>El usuario si existe; de lo contrario, null.</returns>
        Task<Usuario?> ObtenerUsuarioPorNombreUsuarioAsync(string nombreUsuario);

        /// <summary>
        /// Obtiene un usuario por su correo electrónico.
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico del usuario.</param>
        /// <returns>El usuario si existe; de lo contrario, null.</returns>
        Task<Usuario?> ObtenerUsuarioPorCorreoElectronicoAsync(string correoElectronico);

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar.</param>
        /// <param name="usuarioDTO">Nuevos datos del usuario.</param>
        /// <returns>El usuario actualizado.</returns>
        Task<Usuario> ActualizarUsuarioAsync(int id, UsuarioDTO usuarioDTO);

        /// <summary>
        /// Elimina un usuario del sistema.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>True si el usuario fue eliminado; de lo contrario, false.</returns>
        Task<bool> EliminarUsuarioAsync(int id);

        /// <summary>
        /// Autentica un usuario usando su nombre de usuario/correo y contraseña.
        /// </summary>
        /// <param name="nombreUsuarioOCorreo">Nombre de usuario o correo electrónico.</param>
        /// <param name="contraseña">Contraseña del usuario.</param>
        /// <returns>El usuario autenticado si las credenciales son correctas; de lo contrario, null.</returns>
        Task<Usuario?> AutenticarUsuarioAsync(string nombreUsuarioOCorreo, string contraseña);

        /// <summary>
        /// Inicia el proceso de restablecimiento de contraseña.
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico del usuario.</param>
        /// <returns>True si el proceso fue iniciado correctamente; de lo contrario, false.</returns>
        Task<bool> IniciarRestablecimientoContraseñaAsync(string correoElectronico);

        /// <summary>
        /// Restablece la contraseña de un usuario usando un token.
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico del usuario.</param>
        /// <param name="token">Token de restablecimiento.</param>
        /// <param name="nuevaContraseña">Nueva contraseña.</param>
        /// <returns>True si la contraseña fue restablecida; de lo contrario, false.</returns>
        Task<bool> RestablecerContraseñaAsync(string correoElectronico, string token, string nuevaContraseña);

        /// <summary>
        /// Obtiene todos los usuarios del sistema.
        /// </summary>
        /// <returns>Lista de todos los usuarios.</returns>
        Task<IEnumerable<Usuario>> ObtenerTodosUsuariosAsync();

        /// <summary>
        /// Verifica si un nombre de usuario ya está en uso.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario a verificar.</param>
        /// <param name="id">ID del usuario (opcional, para verificar la unicidad en actualizaciones).</param>
        /// <returns>True si el nombre de usuario ya existe; de lo contrario, false.</returns>
        Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, int? id = null);

        /// <summary>
        /// Verifica si un correo electrónico ya está en uso.
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico a verificar.</param>
        /// <param name="id">ID del usuario (opcional, para verificar la unicidad en actualizaciones).</param>
        /// <returns>True si el correo electrónico ya existe; de lo contrario, false.</returns>
        Task<bool> ExisteCorreoElectronicoAsync(string correoElectronico, int? id = null);

        /// <summary>
        /// Envía un correo de bienvenida a un usuario recién registrado.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <returns>True si el correo fue enviado; de lo contrario, false.</returns>
        Task<bool> EnviarCorreoBienvenidaAsync(int usuarioId);

        /// <summary>
        /// Envía una notificación de actividad en la cuenta del usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <param name="detallesActividad">Detalles de la actividad a notificar.</param>
        /// <returns>True si la notificación fue enviada; de lo contrario, false.</returns>
        Task<bool> EnviarNotificacionActividadAsync(int usuarioId, string detallesActividad);
    }
}