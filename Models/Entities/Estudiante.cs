namespace GestionAcademicaAPI.Models.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string ApellidoPat { get; set; }
        public required string ApellidoMat { get; set; }
        public required string EmailEscolar { get; set; }
        public string? InePdf { get; set; }
        public required int Boleta { get; set; }
        public required string Carrera { get; set; }
        public required int IdUsuario { get; set; }
        public required Usuario Usuario { get; set; }
    }
}