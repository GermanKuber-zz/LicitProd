using System;

namespace LicitProd.Entities
{

    public class Concurso : Verificable
    {
        public int Status { get; set; }
        public decimal Presupuesto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; } = DateTime.Now.AddDays(1);
        public DateTime FechaApertura { get; set; } = DateTime.Now.AddDays(2);
        public bool AdjudicacionDirecta { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        public Concurso(decimal presupuesto,
            string nombre,
            DateTime fechaInicio,
            DateTime fechaApertura,
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
            return HashValue(Status +
                             Presupuesto.ToString() + 
                             Nombre +
                             FechaInicio +
                             FechaApertura +
                             AdjudicacionDirecta +
                             Descripcion);
        }
    }
}
