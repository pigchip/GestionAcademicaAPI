using MyProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAcademicaAPI.Models.Entities
{
    /// <summary>
    /// Representa un registro de envío de correo.
    /// </summary>
    public class RegistroEnvioCorreo
    {
        /// <summary>
        /// Obtiene o establece el identificador único del registro.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del usuario que envió el correo.
        /// </summary>
        [Required]
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

        /// <summary>
        /// Obtiene o establece el usuario asociado al registro de envío de correo.
        /// </summary>
        [ForeignKey("IdUsuario")]
        public required Usuario Usuario { get; set; }
    }
}
