using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Contenido { get; set; }

        [Required]
        public int IdMateria { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [ForeignKey("IdMateria")]
        public required Materia Materia { get; set; }

        [ForeignKey("IdUsuario")]
        public required Usuario Usuario { get; set; }
    }
}