using System.ComponentModel.DataAnnotations;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para entidad Materia.
    /// </summary>
    public class MateriaDTO
    {
        /// <summary>
        /// Obtiene o establece el nombre de la materia en ESCOM.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la materia ESCOM es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la materia ESCOM no puede exceder los 100 caracteres.")]
        public required string NombreMateriaEscom { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la materia en la institución foránea.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la materia foránea es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la materia foránea no puede exceder los 100 caracteres.")]
        public required string NombreMateriaForanea { get; set; }

        /// <summary>
        /// Obtiene o establece la URL del temario de la materia foránea.
        /// </summary>
        public string? TemarioMateriaForaneaUrl { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la materia.
        /// </summary>
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede tener más de 50 caracteres.")]
        public required string Status { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del estudiante asociado a la materia.
        /// </summary>
        [Required(ErrorMessage = "El ID del estudiante es obligatorio.")]
        public int IdEstudiante { get; set; }
    }
}

