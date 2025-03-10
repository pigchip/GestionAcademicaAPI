using System;
using System.Collections.Generic;

namespace GestionAcademicaAPI.Models.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string EmailPersonal { get; set; }
        public required string Password { get; set; }

        public Administrador? Administrador { get; set; }
        public Estudiante? Estudiante { get; set; }
        public required List<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
