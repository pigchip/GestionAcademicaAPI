using System.ComponentModel.DataAnnotations;
using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para entidad Estudiante.
    /// </summary>
    public class EstudianteDTO : UsuarioDTO
    {
        /// <summary>
        /// Identificador del usuario asociado al administrador.
        /// </summary>
        [Required(ErrorMessage = "El identificador del usuario es obligatorio.")]
        public int IdUsuario { get; set; }

        /// <summary>
        /// Nombre del estudiante.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public required string Nombre { get; set; }

        /// <summary>
        /// Apellido paterno del estudiante.
        /// </summary>
        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido paterno no puede exceder los 50 caracteres.")]
        public required string ApellidoPat { get; set; }

        /// <summary>
        /// Apellido materno del estudiante.
        /// </summary>
        [Required(ErrorMessage = "El apellido materno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido materno no puede exceder los 50 caracteres.")]
        public required string ApellidoMat { get; set; }

        /// <summary>
        /// Correo electrónico escolar del estudiante.
        /// </summary>
        [Required(ErrorMessage = "El correo electrónico escolar es obligatorio.")]
        [StringLength(100, ErrorMessage = "El correo electrónico escolar no puede exceder los 100 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo electrónico escolar no esta en un formato válido.")]
        public required string EmailEscolar { get; set; }

        /// <summary>
        /// Archivo PDF del INE del estudiante.
        /// </summary>
        public string? InePdf { get; set; }

        /// <summary>
        /// Número de boleta del estudiante.
        /// </summary>
        [Required(ErrorMessage = "El número de boleta es obligatorio.")]
        public int Boleta { get; set; }

        /// <summary>
        /// Carrera del estudiante.
        /// </summary>
        [Required(ErrorMessage = "La carrera es obligatoria.")]
        [StringLength(100, ErrorMessage = "La carrera no puede exceder los 100 caracteres.")]
        public required string Carrera { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de solicitudes realizadas por el estudiante.
        /// </summary>
        public ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();

        /// <summary>
        /// Obtiene o establece la colección de materias asociadas al estudiante.
        /// </summary>
        public ICollection<Materia> Materias { get; set; } = new List<Materia>();
    }
}
