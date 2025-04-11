namespace GestionAcademicaAPI.Models
{
    public class UpdateTemarioDto
    {
        public int Id { get; set; } // ID de la materia a actualizar
        public string TemarioMateriaForaneaUrl { get; set; } // URL del temario
        public string Status { get; set; } // Estado de la materia
    }
    public class UpdateStatusDto
    {
        public int Id { get; set; } // ID de la materia a actualizar
        public string Status { get; set; } // Estado de la materia
    }
}