using System;

namespace LicitProd.Entities
{
    public class Oferta : Entity
    {
        private int concursoId;
        private int proveedorId;

        public decimal Monto { get; set; }
        public string Detalle { get; set; }
        public Concurso Concurso { get; set; }
        public Proveedor Proveedor { get; set; }
        public int ConcursoId { get => Concurso.Id; set => concursoId = value; }
        public int ProveedorId { get => Proveedor.Id; set => proveedorId = value; }

        public Oferta(decimal monto, string detalle, Concurso concurso, Proveedor proveedor)
        {
            Monto = monto;
            Detalle = detalle ?? throw new ArgumentNullException(nameof(detalle));
            Concurso = concurso ?? throw new ArgumentNullException(nameof(concurso));
            Proveedor = proveedor ?? throw new ArgumentNullException(nameof(proveedor));
        }

        public Oferta()
        {
        }
    }
}
