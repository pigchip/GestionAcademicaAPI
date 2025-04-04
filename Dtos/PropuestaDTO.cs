using System.ComponentModel.DataAnnotations;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para entidad Propuesta.
    /// </summary>
    public class PropuestaDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador de la solicitud asociada.
        /// </summary>
        [Required(ErrorMessage = "El ID de la solicitud es obligatorio.")]
        public int IdSolicitud { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la escuela asociada.
        /// </summary>
        [Required(ErrorMessage = "El ID de la escuela es obligatorio.")]
        public int IdEscuela { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la propuesta.
        /// </summary>
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede tener más de 50 caracteres.")]
        public required string Status { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha en que se realizó la propuesta.
        /// </summary>
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de materias asociadas a la propuesta.
        /// </summary>
        [Required(ErrorMessage = "La lista de materias es obligatoria.")]
        public ICollection<MateriaDTO> Materias { get; set; } = new List<MateriaDTO>();
    }
}
