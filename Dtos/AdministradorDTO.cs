using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.DTOs
{
    /// <summary>
    /// DTO para representar información de un administrador.
    /// </summary>
    public class AdministradorDTO
    {
        /// <summary>
        /// Identificador del usuario asociado al administrador.
        /// </summary>
        [Required(ErrorMessage = "El identificador del usuario es obligatorio.")]
        public int IdUsuario { get; set; }

        /// <summary>
        /// Información del usuario asociado al administrador.
        /// </summary>
        [Required(ErrorMessage = "La información del usuario es obligatoria.")]
        public required UsuarioDTO Usuario { get; set; }
    }
}
