using System.ComponentModel.DataAnnotations;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para entidad Escuela.
    /// </summary>
    public class EscuelaDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la escuela.
        /// </summary>
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
        public ICollection<PropuestaDTO> Propuestas { get; set; } = new List<PropuestaDTO>();
    }
}

