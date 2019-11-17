using System;

namespace LicitProd.Entities
{
    public class Oferta : Entity
    {

        public decimal Monto { get; set; }
        public string Detalle { get; set; }
        public  int ConcursoProveedorId { get; set; }
        public Oferta(decimal monto, string detalle, int concursoProveedorId)
        {
            Monto = monto;
            Detalle = detalle ?? throw new ArgumentNullException(nameof(detalle));
            ConcursoProveedorId = concursoProveedorId;
        }

        public Oferta()
        {
        }
    }
}
