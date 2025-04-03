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
        /// Username del usuario.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Password del usuario.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la solicitud asociada.
        /// </summary>
        public int IdSolicitud { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la escuela asociada.
        /// </summary>
        public int IdEscuela { get; set; }

        /// <summary>
        /// Obtiene Escuela asociada a la propuesta.
        /// </summary>
        [Required]
        public required Escuela Escuela { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la propuesta.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string Status { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha en que se realizó la propuesta.
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de materias asociadas a la propuesta.
        /// </summary>
        public ICollection<PropuestaMateriaDTO> PropuestaMaterias { get; set; } = new List<PropuestaMateriaDTO>();
    }
}
