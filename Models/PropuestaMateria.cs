using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAcademicaAPI.Models
{
    /// <summary>
    /// Representa la relación entre una propuesta y una materia.
    /// </summary>
    public class PropuestaMateria
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la relación.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la propuesta.
        /// </summary>
        [Required]
        public int IdPropuesta { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la materia.
        /// </summary>
        [Required]
        public int IdMateria { get; set; }

        /// <summary>
        /// Obtiene o establece la propuesta asociada a la relación.
        /// </summary>
        [ForeignKey("IdPropuesta")]
        public required Propuesta Propuesta { get; set; }

        /// <summary>
        /// Obtiene o establece la materia asociada a la relación.
        /// </summary>
        [ForeignKey("IdMateria")]
        public required Materia Materia { get; set; }
    }
}