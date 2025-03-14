using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Propuesta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdSolicitud { get; set; }

        [Required]
        public int IdEscuela { get; set; }

        [Required]
        [StringLength(50)]
        public required string Status { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [ForeignKey("IdSolicitud")]
        public required Solicitud Solicitud { get; set; }

        [ForeignKey("IdEscuela")]
        public required Escuela Escuela { get; set; }

        public ICollection<PropuestaMateria> PropuestaMaterias { get; set; } = new List<PropuestaMateria>();
    }
}