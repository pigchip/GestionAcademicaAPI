using MyProject.Models;

namespace MyProject.Repositories.Interfaces
{
    /// <summary>
    /// Tipos de correos electrónicos que se pueden enviar
    /// </summary>
    public enum TipoCorreoElectronico
    {
        /// <summary>
        /// Restablecimiento de contraseña
        /// </summary>
        RestablecimientoContraseña,

        /// <summary>
        /// Bienvenida
        /// </summary>
        Bienvenida,

        /// <summary>
        /// Confirmación de registro
        /// </summary>
        ConfirmacionRegistro,

        /// <summary>
        /// Cambio de contraseña
        /// </summary>
        CambioContraseña,

        /// <summary>
        /// Notificación de actividad
        /// </summary>
        NotificacionActividad
    }

    /// <summary>
    /// Interfaz para el repositorio de usuarios
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Obtiene un usuario por su identificador único
        /// </summary>
        Task<Usuario?> ObtenerPorIdAsync(int id);

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario
        /// </summary>
        Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);

        /// <summary>
        /// Obtiene un usuario por su correo electrónico
        /// </summary>
        Task<Usuario?> ObtenerPorCorreoElectronicoAsync(string correoElectronico);

        /// <summary>
        /// Crea un nuevo usuario en el sistema
        /// </summary>
        Task<Usuario> CrearAsync(Usuario usuario);

        /// <summary>
        /// Actualiza la información de un usuario existente
        /// </summary>
        Task<Usuario> ActualizarAsync(Usuario usuario);

        /// <summary>
        /// Elimina un usuario del sistema
        /// </summary>
        Task<bool> EliminarAsync(int id);

        /// <summary>
        /// Comprueba si un nombre de usuario ya existe
        /// </summary>
        Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario);

        /// <summary>
        /// Comprueba si un correo electrónico ya existe
        /// </summary>
        Task<bool> ExisteCorreoElectronicoAsync(string correoElectronico);

        /// <summary>
        /// Autentica un usuario
        /// </summary>
        Task<Usuario?> AutenticarAsync(string nombreUsuario, string contraseña);

        /// <summary>
        /// Inicia el proceso de restablecimiento de contraseña
        /// </summary>
        Task<bool> IniciarProcesoRestablecimientoContraseñaAsync(string correoElectronico);

        /// <summary>
        /// Restablece la contraseña de un usuario
        /// </summary>
        Task<bool> RestablecerContraseñaAsync(string correoElectronico, string token, string nuevaContraseña);

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();

        /// <summary>
        /// Envía un correo electrónico automatizado al usuario
        /// </summary>
        /// <param name="usuario">Usuario destinatario</param>
        /// <param name="tipoCorreo">Tipo de correo a enviar</param>
        /// <param name="parametrosAdicionales">Parámetros para personalizar el correo</param>
        Task<bool> EnviarCorreoAutomatizadoAsync(
            Usuario usuario,
            TipoCorreoElectronico tipoCorreo,
            Dictionary<string, string>? parametrosAdicionales = null
        );

        /// <summary>
        /// Registra un intento de envío de correo electrónico
        /// </summary>
        /// <param name="usuario">Usuario destinatario</param>
        /// <param name="tipoCorreo">Tipo de correo enviado</param>
        /// <param name="resultado">Resultado del envío</param>
        /// <param name="detallesError">Detalles del error en caso de fallo</param>
        Task<bool> RegistrarIntentoCorreoAsync(
            Usuario usuario,
            TipoCorreoElectronico tipoCorreo,
            bool resultado,
            string? detallesError = null
        );

        /// <summary>
        /// Genera un token de restablecimiento de contraseña
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico del usuario</param>
        Task<string> GenerarTokenRestablecimientoAsync(string correoElectronico);
    }
}