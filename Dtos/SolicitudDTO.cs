﻿using GestionAcademicaAPI.Dtos;
using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Dtos
{
    public class SolicitudDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador del estudiante que realizó la solicitud.
        /// </summary>
        [Required(ErrorMessage = "El ID del estudiante es obligatorio.")]
        public int IdEstudiante { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la solicitud.
        /// </summary>
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede tener más de 50 caracteres.")]
        public required string Status { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha en que se realizó la solicitud.
        /// </summary>
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la escuela asociada a la solicitud.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la escuela es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la escuela no puede exceder los 100 caracteres.")]
        public required string NombreEscuela { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de propuestas asociadas a la solicitud.
        /// </summary>
        [MaxLength(3, ErrorMessage = "No se pueden tener más de 3 propuestas.")]
        public ICollection<PropuestaDTO> Propuestas { get; set; } = new List<PropuestaDTO>();
    }
}