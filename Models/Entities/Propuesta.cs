using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    /// <summary>
    /// Representa una propuesta realizada por un estudiante.
    /// </summary>
    public class Propuesta
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la propuesta.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la solicitud asociada.
        /// </summary>
        [Required]
        public int IdSolicitud { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la escuela asociada.
        /// </summary>
        [Required]
        public int IdEscuela { get; set; }

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
        /// Obtiene o establece la solicitud asociada a la propuesta.
        /// </summary>
        [ForeignKey("IdSolicitud")]
        public required Solicitud Solicitud { get; set; }

        /// <summary>
        /// Obtiene o establece la escuela asociada a la propuesta.
        /// </summary>
        [ForeignKey("IdEscuela")]
        public required Escuela Escuela { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de materias asociadas a la propuesta.
        /// </summary>
        public ICollection<PropuestaMateria> PropuestaMaterias { get; set; } = new List<PropuestaMateria>();
    }
}