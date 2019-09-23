using System;

namespace LicitProd.Entities
{
    public class Backup: Entity { }
    public class Concurso : Verificable
    {
        public int Status { get; set; }
        public decimal Presupuesto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTimeOffset FechaInicio { get; set; } = DateTime.Now.AddDays(1);
        public DateTimeOffset FechaApertura { get; set; } = DateTime.Now.AddDays(2);
        public bool AdjudicacionDirecta { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public Concurso(decimal presupuesto,
            string nombre,
            DateTimeOffset fechaInicio,
            DateTimeOffset fechaApertura,
            bool adjudicacionDirecta,
            string descripcion)
        {
            Status = 1;
            Presupuesto = presupuesto;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            FechaInicio = fechaInicio;
            FechaApertura = fechaApertura;
            AdjudicacionDirecta = adjudicacionDirecta;
            Descripcion = descripcion;
        }
        public Concurso()
        {

        }

        public override string GenerateHash()
        {
            return HashValue(Status.ToString()) +
                HashValue(Presupuesto.ToString()) +
                HashValue(Nombre.ToString()) +
                HashValue(FechaInicio.ToString()) +
                HashValue(FechaApertura.ToString()) +
                HashValue(AdjudicacionDirecta.ToString()) +
                HashValue(Descripcion.ToString());
        }
    }
}
