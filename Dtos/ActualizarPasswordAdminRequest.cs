using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.DTOs
{
    /// <summary>
    /// Representa una solicitud para actualizar la contrase�a de un usuario
    /// </summary>
    public class ActualizarPasswordAdminRequest
    {
        /// <summary>
        /// El nombre de usuario para autenticar
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public required string Username { get; set; }

        /// <summary>
        /// La contrase�a actual para autenticar
        /// </summary>
        [Required(ErrorMessage = "La contrase�a actual es obligatoria")]
        public required string Password { get; set; }

        /// <summary>
        /// La nueva contrase�a
        /// </summary>
        [Required(ErrorMessage = "La nueva contrase�a es obligatoria")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "La contrase�a debe tener entre 5 y 20 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{5,}$", 
            ErrorMessage = "La contrase�a debe tener al menos una letra may�scula, una letra min�scula, un n�mero y un car�cter especial")]
        public required string NuevaPassword { get; set; }
    }
}
