using System.ComponentModel.DataAnnotations;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para entidad RegistroEnvioCorreo.
    /// </summary>
    public class RegistroEnvioCorreoDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador único del registro.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del usuario que envió el correo.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Obtiene o establece el tipo de correo enviado.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string TipoCorreo { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de envío del correo.
        /// </summary>
        [Required]
        public DateTime FechaEnvio { get; set; }

        /// <summary>
        /// Obtiene o establece el resultado del envío del correo.
        /// </summary>
        [Required]
        public bool Resultado { get; set; }

        /// <summary>
        /// Obtiene o establece los detalles del error en caso de fallo en el envío.
        /// </summary>
        public string? DetallesError { get; set; }
    }
}


