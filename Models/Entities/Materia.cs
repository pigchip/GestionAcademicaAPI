using GestionAcademicaAPI.Models.Entities;

namespace MyProject.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public required string NombreMateriaEscom { get; set; }
        public required string NombreMateriaForanea { get; set; }
        public string? TemarioMateriaForaneaUrl { get; set; }
        public required string Status { get; set; }
        public required int IdEstudiante { get; set; }
        public required Estudiante Estudiante { get; set; }
    }
}