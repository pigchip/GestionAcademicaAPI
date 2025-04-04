using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para la solicitud de entrada que representa la creación de una solicitud académica.
    /// </summary>
    public class SolicitudRequestDto
    {
        /// <summary>
        /// Obtiene o establece el identificador del estudiante que realiza la solicitud.
        /// </summary>
        [Required(ErrorMessage = "El ID del estudiante es obligatorio.")]
        public int IdEstudiante { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la escuela asociada a la solicitud.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la escuela es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la escuela no puede exceder los 100 caracteres.")]
        public required string NombreEscuela { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de propuestas asociadas a la solicitud.
        /// </summary>
        [Required(ErrorMessage = "La lista de propuestas es obligatoria.")]
        [MaxLength(3, ErrorMessage = "No se pueden tener más de 3 propuestas.")]
        public ICollection<PropuestaRequestDTO> Solicitud { get; set; } = new List<PropuestaRequestDTO>();
    }

    /// <summary>
    /// DTO para una propuesta dentro de la solicitud.
    /// </summary>
    public class PropuestaRequestDTO
    {
        /// <summary>
        /// Obtiene o establece la lista de materias asociadas a la propuesta.
        /// </summary>
        [Required(ErrorMessage = "La lista de materias es obligatoria.")]
        public ICollection<MateriaRequestDTO> Propuesta { get; set; } = new List<MateriaRequestDTO>();
    }

    /// <summary>
    /// DTO para una materia dentro de una propuesta.
    /// </summary>
    public class MateriaRequestDTO
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
    }
}