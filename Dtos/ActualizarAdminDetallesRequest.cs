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
        /// La contrase�a para autenticar
        /// </summary>
        [Required(ErrorMessage = "La contrase�a es obligatoria")]
        public required string Password { get; set; }

        /// <summary>
        /// El nuevo correo electr�nico personal
        /// </summary>
        [EmailAddress(ErrorMessage = "El correo electr�nico no tiene un formato v�lido")]
        [StringLength(50, ErrorMessage = "El correo electr�nico no puede exceder los 50 caracteres")]
        public string? NuevoEmailPersonal { get; set; }
    }
}
