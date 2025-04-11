using System.ComponentModel.DataAnnotations;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para entidad Comentario.
    /// </summary>
    public class ComentarioDTO
    {
        /// <summary>
        /// Obtiene o establece el contenido del comentario.
        /// </summary>
        [Required]
        public required string Contenido { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la solicitud asociada al comentario.
        /// </summary>
        public int IdSolicitud { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del usuario que realizó el comentario.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha en que se realizó el comentario.
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; }
    }
}


