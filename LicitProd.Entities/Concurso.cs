using System;

namespace LicitProd.Entities
{
    public class Concurso : Verificable
    {
        public int Status { get; set; }
        public decimal Presupuesto { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaApertura { get; set; }
        public bool AdjudicacionDirecta { get; set; }
        public string Descripcion { get; set; }
        public Concurso(int status,
            decimal presupuesto,
            string nombre,
            DateTime fechaInicio,
            DateTime fechaApertura,
            bool adjudicacionDirecta,
            string descripcion)
        {
            Status = status;
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
