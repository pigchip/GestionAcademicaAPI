using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.DTOs
{
    /// <summary>
    /// Representa una solicitud para actualizar el nombre de usuario
    /// </summary>
    public class ActualizarUsernameAdminRequest
    {
        /// <summary>
        /// El nombre de usuario actual para autenticar
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario actual es obligatorio")]
        public required string Username { get; set; }

        /// <summary>
        /// La contraseña para autenticar
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public required string Password { get; set; }

        /// <summary>
        /// El nuevo nombre de usuario
        /// </summary>
        [Required(ErrorMessage = "El nuevo nombre de usuario es obligatorio")]
        [StringLength(10, ErrorMessage = "El nombre de usuario no puede exceder los 10 caracteres")]
        public required string NuevoUsername { get; set; }
    }
}
