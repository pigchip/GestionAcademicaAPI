using GestionAcademicaAPI.DTOs;
using GestionAcademicaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para entidad Estudiante.
    /// </summary>
    public class EstudianteDTO : UsuarioDTO
    {
        /// <summary>
        /// Nombre del estudiante.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }

        /// <summary>
        /// Apellido paterno del estudiante.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string ApellidoPat { get; set; }

        /// <summary>
        /// Apellido materno del estudiante.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string ApellidoMat { get; set; }

        /// <summary>
        /// Correo electrónico escolar del estudiante.
        /// </summary>
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public required string EmailEscolar { get; set; }

        /// <summary>
        /// Archivo PDF del INE del estudiante.
        /// </summary>
        public string? InePdf { get; set; }

        /// <summary>
        /// Número de boleta del estudiante.
        /// </summary>
        [Required]
        public int Boleta { get; set; }

        /// <summary>
        /// Carrera del estudiante.
        /// </summary>
        [Required]
        [StringLength(100)]
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
