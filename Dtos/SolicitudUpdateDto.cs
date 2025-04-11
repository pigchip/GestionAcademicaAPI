using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para actualizar el estado de una solicitud.
    /// </summary>
    public class SolicitudUpdateDto
    {
        /// <summary>
        /// Identificador único de la solicitud a actualizar.
        /// </summary>
        [Required(ErrorMessage = "El ID de la solicitud es obligatorio.")]
        public int IdSolicitud { get; set; }

        /// <summary>
        /// Nuevo estado de la solicitud.
        /// </summary>
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres.")]
        public string Status { get; set; }
    }
}