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
        /// Obtiene o establece el identificador único de la materia.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la materia en ESCOM.
        /// </summary>
        [Required]
        [StringLength(100)]
        public required string NombreMateriaEscom { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la materia en la institución foránea.
        /// </summary>
        [Required]
        [StringLength(100)]
        public required string NombreMateriaForanea { get; set; }

        /// <summary>
        /// Obtiene o establece la URL del temario de la materia foránea.
        /// </summary>
        public string? TemarioMateriaForaneaUrl { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la materia.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string Status { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador del estudiante asociado a la materia.
        /// </summary>
        public int IdEstudiante { get; set; }
    }
}

