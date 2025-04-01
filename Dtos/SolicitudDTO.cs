using System.ComponentModel.DataAnnotations;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para entidad Solicitud.
    /// </summary>
    public class SolicitudDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la solicitud.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del estudiante que realizó la solicitud.
        /// </summary>
        public int IdEstudiante { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la solicitud.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string Status { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha en que se realizó la solicitud.
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de propuestas asociadas a la solicitud.
        /// </summary>
        public ICollection<PropuestaDTO> Propuestas { get; set; } = new List<PropuestaDTO>();
    }
}
