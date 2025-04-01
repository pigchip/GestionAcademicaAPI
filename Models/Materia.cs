using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAcademicaAPI.Models
{
    /// <summary>
    /// Representa una materia en el sistema.
    /// </summary>
    public class Materia
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la materia.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la materia en ESCOM.
        /// </summary>
        [Required]
        [StringLength(100)]
        public required string NombreMateriaEscom { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la materia en la institución foránea.
        /// </summary>
        [Required]
        [StringLength(100)]
        public required string NombreMateriaForanea { get; set; }

        /// <summary>
        /// Obtiene o establece la URL del temario de la materia foránea.
        /// </summary>
        public string? TemarioMateriaForaneaUrl { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la materia.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string Status { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del estudiante asociado a la materia.
        /// </summary>
        [Required]
        public int IdEstudiante { get; set; }

        /// <summary>
        /// Obtiene o establece el estudiante asociado a la materia.
        /// </summary>
        [ForeignKey("IdEstudiante")]
        public required Estudiante Estudiante { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de propuestas de materia asociadas.
        /// </summary>
        public ICollection<PropuestaMateria> PropuestaMaterias { get; set; } = new List<PropuestaMateria>();

        /// <summary>
        /// Obtiene o establece la colección de comentarios asociados a la materia.
        /// </summary>
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}