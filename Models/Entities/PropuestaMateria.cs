using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class PropuestaMateria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdPropuesta { get; set; }

        [Required]
        public int IdMateria { get; set; }

        [ForeignKey("IdPropuesta")]
        public required Propuesta Propuesta { get; set; }

        [ForeignKey("IdMateria")]
        public required Materia Materia { get; set; }
    }
}