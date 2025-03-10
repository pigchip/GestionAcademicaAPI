namespace GestionAcademicaAPI.Models.Entities
{
    public class Administrador
    {
        public int Id { get; set; }
        public required int IdUsuario { get; set; }
        public required Usuario Usuario { get; set; }
    }
}
