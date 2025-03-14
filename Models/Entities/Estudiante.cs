using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public required string ApellidoPat { get; set; }

        [Required]
        [StringLength(50)]
        public required string ApellidoMat { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public required string EmailEscolar { get; set; }

        public string? InePdf { get; set; }

        [Required]
        public int Boleta { get; set; }

        [Required]
        [StringLength(100)]
        public required string Carrera { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public required Usuario Usuario { get; set; }

        public ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();
        public ICollection<Materia> Materias { get; set; } = new List<Materia>();
    }
}