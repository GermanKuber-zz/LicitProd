using System;

namespace LicitProd.Entities
{
    public class PreguntaConcurso : Entity
    {
        public string Pregunta { get; set; }
        public string Respuesta { get; set; } = String.Empty;
        public int ConcursoProveedorId { get; set; }
    }
}