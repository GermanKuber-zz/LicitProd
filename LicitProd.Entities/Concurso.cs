using System;
using System.Collections.Generic;

namespace LicitProd.Entities
{
    public enum ConcursoStatusEnum
    {
        Borrador = 0,
        Nuevo = 1,
        Open = 2,
        Cancelado = 3,
        Cerrado = 4
    }
    public class Concurso : Verificable
    {
        public int Status { get; set; }
        public decimal Presupuesto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; } = DateTime.Now.AddDays(1);
        public DateTime FechaApertura { get; set; } = DateTime.Now.AddDays(2);
        public bool AdjudicacionDirecta { get; set; }
        public bool Borrador { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int TerminosYCondicionesId { get; set; }
        public int CompradorId { get; set; }
        public Comprador Comprador { get; set; }
        public List<ConcursoProveedor> ConcursoProveedores { get; set; } =new List<ConcursoProveedor>();
        public Concurso(decimal presupuesto,
            string nombre,
            DateTime fechaInicio,
            DateTime fechaApertura,
            bool adjudicacionDirecta,
            string descripcion)
        {
            Status = (int)ConcursoStatusEnum.Nuevo;
            Presupuesto = presupuesto;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            if (fechaInicio <= DateTime.Now.AddDays(2))
                throw new Exception("La fecha de inicio debe de ser mayor a 2 días.");
            FechaInicio = fechaInicio;
            FechaApertura = fechaApertura;
            AdjudicacionDirecta = adjudicacionDirecta;
            Descripcion = descripcion;
        }
        public Concurso()
        {
            Status = (int)ConcursoStatusEnum.Nuevo;
        }


        public override string GenerateHash()
        {
            return HashValue(Status +
                             Presupuesto.ToString() +
                             Nombre +
                             FechaInicio.ToShortDateString() +
                             FechaApertura.ToShortDateString() +
                             AdjudicacionDirecta +
                             Descripcion);
        }
    }
}
