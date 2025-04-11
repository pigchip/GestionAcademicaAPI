using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.DTOs
{
    /// <summary>
    /// Representa una solicitud para actualizar la contraseña de un usuario
    /// </summary>
    public class ActualizarPasswordAdminRequest
    {
        /// <summary>
        /// El nombre de usuario para autenticar
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public required string Username { get; set; }

        /// <summary>
        /// La contraseña actual para autenticar
        /// </summary>
        [Required(ErrorMessage = "La contraseña actual es obligatoria")]
        public required string Password { get; set; }

        /// <summary>
        /// La nueva contraseña
        /// </summary>
        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "La contraseña debe tener entre 5 y 20 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{5,}$", 
            ErrorMessage = "La contraseña debe tener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial")]
        public required string NuevaPassword { get; set; }
    }
}
