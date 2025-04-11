using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para actualizar el estado de una propuesta.
    /// </summary>
    public class PropuestaUpdateDto
    {
        /// <summary>
        /// Identificador único de la propuesta a actualizar.
        /// </summary>
        [Required(ErrorMessage = "El ID de la propuesta es obligatorio.")]
        public int IdPropuesta { get; set; }

        /// <summary>
        /// Nuevo estado de la propuesta.
        /// </summary>
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres.")]
        public string Status { get; set; }
    }
}