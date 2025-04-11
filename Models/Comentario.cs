using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAcademicaAPI.Models
{
    /// <summary>
    /// Representa un comentario en el sistema.
    /// </summary>
    public class Comentario
    {
        /// <summary>
        /// Obtiene o establece el identificador único del comentario.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el contenido del comentario.
        /// </summary>
        [Required]
        public required string Contenido { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la solicitud asociada al comentario.
        /// </summary>
        [Required]
        public int IdSolicitud { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del usuario que realizó el comentario.
        /// </summary>
        [Required]
        public int IdUsuario { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha en que se realizó el comentario.
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Obtiene o establece la solicitud asociada al comentario.
        /// </summary>
        [ForeignKey("IdSolicitud")]
        public Solicitud? Solicitud { get; set; }

        /// <summary>
        /// Obtiene o establece el usuario que realizó el comentario.
        /// </summary>
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
    }
}