using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public required string EmailPersonal { get; set; }

        [Required]
        public required string Password { get; set; }

        public Administrador? Administrador { get; set; }
        public Estudiante? Estudiante { get; set; }
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}