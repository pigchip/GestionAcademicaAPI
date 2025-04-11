using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.DTOs
{
    /// <summary>
    /// Representa una solicitud para actualizar los detalles de un administrador
    /// </summary>
    public class ActualizarAdminDetallesRequest
    {
        /// <summary>
        /// El nombre de usuario para autenticar
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public required string Username { get; set; }

        /// <summary>
        /// La contraseña para autenticar
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public required string Password { get; set; }

        /// <summary>
        /// El nuevo correo electrónico personal
        /// </summary>
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        [StringLength(50, ErrorMessage = "El correo electrónico no puede exceder los 50 caracteres")]
        public string? NuevoEmailPersonal { get; set; }
    }
}
