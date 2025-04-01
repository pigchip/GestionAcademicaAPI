using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Models
{
    /// <summary>
    /// Representa una escuela en el sistema.
    /// </summary>
    public class Escuela
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la escuela.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la escuela.
        /// </summary>
        [Required]
        [StringLength(100)]
        public required string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de propuestas asociadas a la escuela.
        /// </summary>
        public ICollection<Propuesta> Propuestas { get; set; } = new List<Propuesta>();
    }
}