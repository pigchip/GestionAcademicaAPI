using System.ComponentModel.DataAnnotations;
using GestionAcademicaAPI.Models;

namespace GestionAcademicaAPI.Dtos
{
    /// <summary>
    /// DTO para la relación entre una propuesta y una materia.
    /// </summary>
    public class PropuestaMateriaDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador de la propuesta.
        /// </summary>
        public int IdPropuesta { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador de la materia.
        /// </summary>
        public int IdMateria { get; set; }
    }
}

