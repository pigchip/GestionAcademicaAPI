using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Models
{
    /// <summary>
    /// Representa un usuario en el sistema.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Obtiene o establece el identificador único del usuario.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de usuario.
        /// </summary>
        [Required]
        [StringLength(10)]
        public required string Username { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico personal del usuario.
        /// </summary>
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public required string EmailPersonal { get; set; }

        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        [Required]
        public required string Password { get; set; }

        /// <summary>
        /// Obtiene o establece el token utilizado para restablecer la contraseña.
        /// </summary>
        [StringLength(500)]
        public string? ResetPasswordToken { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de expiración del token de restablecimiento de contraseña.
        /// </summary>
        public DateTime? ResetPasswordTokenExpiration { get; set; }

        /// <summary>
        /// Obtiene o establece los detalles del administrador asociado, si los hay.
        /// </summary>
        public Administrador? Administrador { get; set; }

        /// <summary>
        /// Obtiene o establece los detalles del estudiante asociado, si los hay.
        /// </summary>
        public Estudiante? Estudiante { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de comentarios realizados por el usuario.
        /// </summary>
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

        /// <summary>
        /// Obtiene o establece la colección de intentos de envío de correo realizados por el usuario.
        /// </summary>
        public ICollection<RegistroEnvioCorreo> RegistroEnvioCorreos { get; set; } = new List<RegistroEnvioCorreo>();
    }
}