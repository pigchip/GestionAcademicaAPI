using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAcademicaAPI.Models
{
    /// <summary>
    /// Representa un administrador en el sistema.
    /// </summary>
    public class Administrador
    {
        /// <summary>
        /// Obtiene o establece el identificador único del administrador.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del usuario asociado al administrador.
        /// </summary>
        [Required]
        public int IdUsuario { get; set; }

        /// <summary>
        /// Obtiene o establece el usuario asociado al administrador.
        /// </summary>
        [ForeignKey("IdUsuario")]
        public required Usuario Usuario { get; set; }
    }
}