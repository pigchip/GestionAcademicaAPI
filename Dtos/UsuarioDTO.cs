using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.DTOs
{
    /// <summary>
    /// DTO para entidad Usuario.
    /// </summary>
    public class UsuarioDTO
    {
        /// <summary>
        /// Nombre de usuario para el registro.
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(10, ErrorMessage = "El nombre de usuario no puede exceder los 10 caracteres.")]
        public required string Username { get; set; }

        /// <summary>
        /// Correo electrónico del usuario para el registro.
        /// </summary>
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [StringLength(50, ErrorMessage = "El correo electrónico no puede exceder los 50 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public required string EmailPersonal { get; set; }

        /// <summary>
        /// Contraseña para el registro.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "La contraseña debe tener entre 5 y 20 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{5,}$", ErrorMessage = "La contraseña debe tener entre 5 y 20 caracteres, tener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
        public required string Password { get; set; }
    }
}