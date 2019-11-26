using System;
using System.Collections.Generic;
using System.Linq;

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
        public bool Borrador { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int TerminosYCondicionesId { get; set; }
        public int CompradorId { get; set; }
        public Comprador Comprador { get; set; }
        public CentroOperativo CentroOperativo { get; set; }
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

        private Response<bool> ValidateFecha()
        {
            if (FechaInicio < DateTime.Now)
                return Response<bool>.Error();
            return  Response<bool>.Ok(true);
        }

        public Response<bool> Validar()
        {
            if (FechaApertura < FechaInicio && !ValidateFecha().SuccessResult)
                return Response<bool>.Error();
            return Response<bool>.Ok(true);
        }

        public void AgregarProveedores(List<Proveedor> proveedores)
        {
            ConcursoProveedores.AddRange(proveedores.Select(x => new ConcursoProveedor
            {
                Proveedor = x
            }));
        }
    }
}
