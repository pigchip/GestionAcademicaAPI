using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAcademicaAPI.Models
{
    /// <summary>
    /// Representa una solicitud realizada por un estudiante.
    /// </summary>
    public class Solicitud
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la solicitud.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del estudiante que realizó la solicitud.
        /// </summary>
        [Required]
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
        /// Obtiene o establece el estudiante asociado a la solicitud.
        /// </summary>
        [ForeignKey("IdEstudiante")]
        public required Estudiante Estudiante { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de propuestas asociadas a la solicitud.
        /// </summary>
        public ICollection<Propuesta> Propuestas { get; set; } = new List<Propuesta>();
    }
}