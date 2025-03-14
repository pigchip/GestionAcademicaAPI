using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Solicitud
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdEstudiante { get; set; }

        [Required]
        [StringLength(50)]
        public required string Status { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [ForeignKey("IdEstudiante")]
        public required Estudiante Estudiante { get; set; }

        public ICollection<Propuesta> Propuestas { get; set; } = new List<Propuesta>();
    }
}