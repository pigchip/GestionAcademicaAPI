using MyProject.Models;

namespace GestionAcademicaAPI.Models.Entities
{
    public class Comentario
    {
        public int Id { get; set; }
        public required string Contenido { get; set; }
        public required int IdMateria { get; set; }
        public required Materia Materia { get; set; }
        public required int IdUsuario { get; set; }
        public required Usuario Usuario { get; set; }
        public required DateTime Fecha { get; set; }
    }
}