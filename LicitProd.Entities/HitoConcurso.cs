using System;

namespace LicitProd.Entities
{
    public class HitoConcurso : Entity
    {
        public int ConcursoId { get; set; }
        public string Hito { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}