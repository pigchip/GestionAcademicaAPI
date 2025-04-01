using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAcademicaAPI.Models
{
    /// <summary>
    /// Representa un estudiante en el sistema.
    /// </summary>
    public class Estudiante
    {
        /// <summary>
        /// Obtiene o establece el identificador único del estudiante.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del estudiante.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el apellido paterno del estudiante.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string ApellidoPat { get; set; }

        /// <summary>
        /// Obtiene o establece el apellido materno del estudiante.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string ApellidoMat { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico escolar del estudiante.
        /// </summary>
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public required string EmailEscolar { get; set; }

        /// <summary>
        /// Obtiene o establece el archivo PDF del INE del estudiante.
        /// </summary>
        public string? InePdf { get; set; }

        /// <summary>
        /// Obtiene o establece el número de boleta del estudiante.
        /// </summary>
        [Required]
        public int Boleta { get; set; }

        /// <summary>
        /// Obtiene o establece la carrera del estudiante.
        /// </summary>
        [Required]
        [StringLength(100)]
        public required string Carrera { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del usuario asociado al estudiante.
        /// </summary>
        [Required]
        public int IdUsuario { get; set; }

        /// <summary>
        /// Obtiene o establece el usuario asociado al estudiante.
        /// </summary>
        [ForeignKey("IdUsuario")]
        public required Usuario Usuario { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de solicitudes realizadas por el estudiante.
        /// </summary>
        public ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();

        /// <summary>
        /// Obtiene o establece la colección de materias asociadas al estudiante.
        /// </summary>
        public ICollection<Materia> Materias { get; set; } = new List<Materia>();
    }
}