namespace MyProject.Models
{
    public class PropuestaMateria
    {
        public int Id { get; set; }
        public required int IdPropuesta { get; set; }
        public required Propuesta Propuesta { get; set; }
        public required int IdMateria { get; set; }
        public required Materia Materia { get; set; }
    }
}
