using System;

namespace MyProject.Models
{
    public class Propuesta
    {
        public int Id { get; set; }
        public required int IdSolicitud { get; set; }
        public required Solicitud Solicitud { get; set; }
        public required int IdEscuela { get; set; }
        public required Escuela Escuela { get; set; }
        public required string Status { get; set; }
        public required DateTime Fecha { get; set; }
    }
}