using System.Security.Cryptography;
using System.Text;

namespace GestionAcademicaAPI.Helpers
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
    /// Provides helper methods for security-related functions.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Hashes the specified password using SHA-256.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password as a base64-encoded string.</returns>
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        /// <summary>
        /// Verifies that the specified password matches the hashed password.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="hashedPassword">The hashed password to compare against.</param>
        /// <returns><c>true</c> if the password matches the hashed password; otherwise, <c>false</c>.</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }

    /// <summary>
    /// Representa una solicitud para crear un nuevo usuario.
    /// </summary>
    public class CrearUsuarioRequest
    {
        /// <summary>
        /// El nombre de usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// El correo electrónico personal.
        /// </summary>
        public string EmailPersonal { get; set; } = string.Empty;
        /// <summary>
        /// La contraseña.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa una solicitud de inicio de sesión.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// El nombre de usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// La contraseña.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Actualizar Admin
    /// </summary>
    public class ActualizarAdminRequest
    {
        /// <summary>
        /// El ID de Administrador.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// El username de Administrador.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// La password de Administrador
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// El nuevo nombre de Usuario
        /// </summary>
        public string NuevoUsername { get; set; } = string.Empty;
        /// <summary>
        /// La nueva Password
        /// </summary>
        public string NuevaPassword { get; set; } = string.Empty;
        /// <summary>
        /// El correo electrónico personal.
        /// </summary>
        public string NuevoEmailPersonal { get; set; } = string.Empty;
    }

    public class CrearEstudianteRequest
    {
        /// <summary>
        /// El nombre de usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// La contraseña.
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// El correo electrónico escolar.
        /// </summary>
        public string EmailEscolar { get; set; } = string.Empty;
        /// <summary>
        /// El correo electrónico personal.
        /// </summary>
        public string EmailPersonal { get; set; } = string.Empty;
        /// <summary>
        /// El nombre del estudiante.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
        /// <summary>
        /// El apellido paterno del estudiante.
        /// </summary>
        public string ApellidoPaterno { get; set; } = string.Empty;
        /// <summary>
        /// El apellido materno del estudiante.
        /// </summary>
        public string ApellidoMaterno { get; set; } = string.Empty;
        /// <summary>
        /// El semestre en el que se encuentra el estudiante.
        /// </summary>
        public int Semestre { get; set; }
        /// <summary>
        /// El número de boleta.
        /// </summary>
        public int Boleta { get; set; }
        /// <summary>
        /// El nombre de la carrera del estudiante.
        /// </summary>
        public string Carrera { get; set; } = string.Empty;
    }

    /// <summary>
    /// Actualizar Estudiante
    /// </summary>
    public class ActualizarEstudianteRequest
    {
        /// <summary>
        /// El ID de Estudiante.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// El username de Estudiante.
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// La password de Estudiante
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// El nuevo nombre de Usuario
        /// </summary>
        public string NuevoUsername { get; set; } = string.Empty;
        /// <summary>
        /// La nueva Password
        /// </summary>
        public string NuevaPassword { get; set; } = string.Empty;
        /// <summary>
        /// El nuevo correo electrónico escolar.
        /// </summary>
        public string NuevoEmailEscolar { get; set; } = string.Empty;
        /// <summary>
        /// El nuevo correo electrónico personal.
        /// </summary>
        public string NuevoEmailPersonal { get; set; } = string.Empty;
        /// <summary>
        /// El nuevo número de boleta.
        /// </summary>
        public int NuevoBoleta { get; set; }
        /// <summary>
        /// El nuevo nombre del estudiante.
        /// </summary>
        public string NuevoNombre { get; set; } = string.Empty;
        /// <summary>
        /// El nuevo apellido paterno del estudiante.
        /// </summary>
        public string NuevoApellidoPaterno { get; set; } = string.Empty;
        /// <summary>
        /// El nuevo apellido materno del estudiante.
        /// </summary>
        public string NuevoApellidoMaterno { get; set; } = string.Empty;
        /// <summary>
        /// El nuevo semestre en el que se encuentra el estudiante.
        /// </summary>
        public int NuevoSemestre { get; set; }
        /// <summary>
        /// El nuevo nombre de la carrera del estudiante.
        /// </summary>
        public string NuevaCarrera { get; set; } = string.Empty;
    }
}
