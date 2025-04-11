using System;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO de respuesta para una solicitud creada.
    /// </summary>
    public class SolicitudResponseDto
    {
        /// <summary>
        /// Identificador único de la solicitud.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador del estudiante que realizó la solicitud.
        /// </summary>
        public int IdEstudiante { get; set; }

        /// <summary>
        /// Estado actual de la solicitud.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Fecha de creación de la solicitud.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Lista de propuestas asociadas a la solicitud.
        /// </summary>
        public PropuestaList Propuestas { get; set; } = new PropuestaList();

        /// <summary>
        /// Lista de comentarios asociados a la solicitud.
        /// </summary>
        public ComentarioList Comentarios { get; set; } = new ComentarioList();
    }

    /// <summary>
    /// Clase contenedora para la lista de propuestas con $values.
    /// </summary>
    public class PropuestaList
    {
        public PropuestaList()
        {
            Values = new List<PropuestaResponseDto>();
        }

        /// <summary>
        /// Lista de propuestas.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("$values")]
        public List<PropuestaResponseDto> Values { get; set; }
    }

    /// <summary>
    /// DTO de respuesta para una propuesta.
    /// </summary>
    public class PropuestaResponseDto
    {
        /// <summary>
        /// Identificador único de la propuesta.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la escuela asociada a la propuesta.
        /// </summary>
        public string NombreEscuela { get; set; }

        /// <summary>
        /// Estado actual de la propuesta.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Fecha de creación de la propuesta.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Lista de materias asociadas a la propuesta.
        /// </summary>
        public MateriaList Materias { get; set; } = new MateriaList();
    }

    /// <summary>
    /// Clase contenedora para la lista de materias con $values.
    /// </summary>
    public class MateriaList
    {
        public MateriaList()
        {
            Values = new List<MateriaResponseDto>();
        }

        /// <summary>
        /// Lista de materias.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("$values")]
        public List<MateriaResponseDto> Values { get; set; }
    }

    /// <summary>
    /// DTO de respuesta para una materia.
    /// </summary>
    public class MateriaResponseDto
    {
        /// <summary>
        /// Identificador único de la materia.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la materia en ESCOM.
        /// </summary>
        public string NombreMateriaEscom { get; set; }

        /// <summary>
        /// Nombre de la materia en la institución foránea.
        /// </summary>
        public string NombreMateriaForanea { get; set; }

        /// <summary>
        /// URL del temario de la materia foránea (puede ser null).
        /// </summary>
        public string? TemarioMateriaForaneaUrl { get; set; }

        /// <summary>
        /// Estado actual de la materia.
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// Clase auxiliar para la lista de comentarios.
    /// </summary>
    public class ComentarioList
    {
        public List<ComentarioResponseDTO> Values { get; set; } = new List<ComentarioResponseDTO>();
    }

    /// <summary>
    /// DTO para representar un comentario.
    /// </summary>
    public class ComentarioResponseDTO
    {
        /// <summary>
        /// Identificador único del comentario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Contenido del comentario.
        /// </summary>
        public string Contenido { get; set; }

        /// <summary>
        /// Identificador de la solicitud asociada al comentario.
        /// </summary>
        public int IdSolicitud { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó el comentario.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Fecha de creación del comentario.
        /// </summary>
        public DateTime Fecha { get; set; }
    }
}