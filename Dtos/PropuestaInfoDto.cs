using System;
using System.Collections.Generic;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO de respuesta para información de una propuesta.
    /// </summary>
    public class PropuestaInfoDto
    {
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public string NombreEscuela { get; set; }
        public string Status { get; set; }
        public DateTime Fecha { get; set; }
        public MateriasPropuestaList Materias { get; set; } = new MateriasPropuestaList();
    }

    /// <summary>
    /// Clase contenedora para la lista de materias de una propuesta.
    /// </summary>
    public class MateriasPropuestaList
    {
        public MateriasPropuestaList()
        {
            Values = new List<MateriaInfoDto>();
        }

        [System.Text.Json.Serialization.JsonPropertyName("$values")]
        public List<MateriaInfoDto> Values { get; set; }
    }

    /// <summary>
    /// DTO de respuesta para información de una materia en una propuesta.
    /// </summary>
    public class MateriaInfoDto
    {
        public int Id { get; set; }
        public string NombreMateriaEscom { get; set; }
        public string NombreMateriaForanea { get; set; }
        public string? TemarioMateriaForaneaUrl { get; set; }
        public string Status { get; set; }
    }
}