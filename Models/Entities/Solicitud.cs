using GestionAcademicaAPI.Models.Entities;
using System;

namespace MyProject.Models
{
    public class Solicitud
    {
        public int Id { get; set; }
        public required int IdEstudiante { get; set; }
        public required Estudiante Estudiante { get; set; }
        public required string Status { get; set; }
        public required DateTime Fecha { get; set; }
    }
}

