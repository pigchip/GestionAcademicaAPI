using System;
using System.Collections.Generic;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para representar un estudiante con todos sus datos relacionados
    /// </summary>
    public class EstudianteDetalladoDTO
    {
        /// <summary>
        /// Identificador único del estudiante
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del estudiante
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido paterno del estudiante
        /// </summary>
        public string ApellidoPat { get; set; }

        /// <summary>
        /// Apellido materno del estudiante
        /// </summary>
        public string ApellidoMat { get; set; }

        /// <summary>
        /// Correo electrónico escolar del estudiante
        /// </summary>
        public string EmailEscolar { get; set; }

        /// <summary>
        /// Archivo PDF del INE del estudiante
        /// </summary>
        public string InePdf { get; set; }

        /// <summary>
        /// Número de boleta del estudiante
        /// </summary>
        public int Boleta { get; set; }

        /// <summary>
        /// Carrera del estudiante
        /// </summary>
        public string Carrera { get; set; }

        /// <summary>
        /// Identificador del usuario asociado
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Información básica del usuario
        /// </summary>
        public UsuarioInfoDTO Usuario { get; set; }

        /// <summary>
        /// Lista de solicitudes del estudiante
        /// </summary>
        public List<SolicitudInfoDTO> Solicitudes { get; set; } = new List<SolicitudInfoDTO>();

        /// <summary>
        /// Lista de materias del estudiante
        /// </summary>
        public List<MateriaInfoDTO> Materias { get; set; } = new List<MateriaInfoDTO>();
    }

    /// <summary>
    /// DTO para información básica del usuario
    /// </summary>
    public class UsuarioInfoDTO
    {
        /// <summary>
        /// Identificador único del usuario
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Correo electrónico personal
        /// </summary>
        public string EmailPersonal { get; set; }
    }

    /// <summary>
    /// DTO para información básica de una solicitud
    /// </summary>
    public class SolicitudInfoDTO
    {
        /// <summary>
        /// Identificador único de la solicitud
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Estado de la solicitud
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Lista de propuestas asociadas a la solicitud
        /// </summary>
        public List<PropuestaInfoDTO> Propuestas { get; set; } = new List<PropuestaInfoDTO>();

        /// <summary>
        /// Lista de comentarios asociados a la solicitud
        /// </summary>
        public List<ComentarioInfoDTO> Comentarios { get; set; } = new List<ComentarioInfoDTO>();
    }

    /// <summary>
    /// DTO para información básica de una propuesta
    /// </summary>
    public class PropuestaInfoDTO
    {
        /// <summary>
        /// Identificador único de la propuesta
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Estado de la propuesta
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Fecha de la propuesta
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Información de la escuela asociada
        /// </summary>
        public EscuelaInfoDTO Escuela { get; set; }

        /// <summary>
        /// Lista de materias asociadas a la propuesta
        /// </summary>
        public List<MateriaInfoDTO> Materias { get; set; } = new List<MateriaInfoDTO>();
    }

    /// <summary>
    /// DTO para información básica de una escuela
    /// </summary>
    public class EscuelaInfoDTO
    {
        /// <summary>
        /// Identificador único de la escuela
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la escuela
        /// </summary>
        public string Nombre { get; set; }
    }

    /// <summary>
    /// DTO para información básica de una materia
    /// </summary>
    public class MateriaInfoDTO
    {
        /// <summary>
        /// Identificador único de la materia
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la materia en ESCOM
        /// </summary>
        public string NombreMateriaEscom { get; set; }

        /// <summary>
        /// Nombre de la materia en la institución foránea
        /// </summary>
        public string NombreMateriaForanea { get; set; }

        /// <summary>
        /// URL del temario de la materia foránea
        /// </summary>
        public string TemarioMateriaForaneaUrl { get; set; }

        /// <summary>
        /// Estado de la materia
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// DTO para información básica de un comentario
    /// </summary>
    public class ComentarioInfoDTO
    {
        /// <summary>
        /// Identificador único del comentario
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Contenido del comentario
        /// </summary>
        public string Contenido { get; set; }

        /// <summary>
        /// Fecha del comentario
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Información básica del usuario que realizó el comentario
        /// </summary>
        public UsuarioInfoDTO Usuario { get; set; }
    }
}
