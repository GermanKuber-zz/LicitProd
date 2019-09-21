using System;

namespace LicitProd.Entities
{
    public abstract class Verificable : Entity
    {
        public string Hash { get; set; }
    }
    public class Concurso : Verificable
    {
        public int Status { get; set; }
        public decimal Presupuesto { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaApertura { get; set; }
        public bool AdjudicacionDirecta { get; set; }

        public Concurso(int status,
            decimal presupuesto,
             string nombre,
             DateTime fechaInicio,
            DateTime fechaApertura,
            bool adjudicacionDirecta)
        {
            Status = status;
            Presupuesto = presupuesto;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            FechaInicio = fechaInicio;
            FechaApertura = fechaApertura;
            AdjudicacionDirecta = adjudicacionDirecta;
        }
        public Concurso()
        {

        }
    }

    public class Entity : IEntityToDb
    {
        public int Id { get; set; }
    }
}
