using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Materia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string NombreMateriaEscom { get; set; }

        [Required]
        [StringLength(100)]
        public required string NombreMateriaForanea { get; set; }

        public string? TemarioMateriaForaneaUrl { get; set; }

        [Required]
        [StringLength(50)]
        public required string Status { get; set; }

        [Required]
        public int IdEstudiante { get; set; }

        [ForeignKey("IdEstudiante")]
        public required Estudiante Estudiante { get; set; }

        public ICollection<PropuestaMateria> PropuestaMaterias { get; set; } = new List<PropuestaMateria>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}