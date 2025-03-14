using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Escuela
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nombre { get; set; }

        public ICollection<Propuesta> Propuestas { get; set; } = new List<Propuesta>();
    }
}