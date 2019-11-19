namespace LicitProd.Entities
{
    public class ConcursoProveedor : Entity
    {
        public int ProveedorId { get; set; }
        public int ConcursoId { get; set; }
        public int  Status { get; set; }
        public bool Ganador { get; set; }
        public Proveedor Proveedor { get; set; }
        public bool AceptoTerminosYCondiciones { get; set; }
        public Oferta Oferta { get; set; }
        public PreguntaConcurso Pregunta { get; set; }
    }
}