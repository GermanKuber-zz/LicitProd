using System;
using System.Reflection;

namespace LicitProd.Entities
{
    public class CentroOperativo : Entity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class HitoConcurso : Entity
    {
        public int ConcursoId { get; set; }
        public string Hito { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}