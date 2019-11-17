namespace LicitProd.Entities
{
    public enum ProveedorConcursoStatusEnum
    {
        Invitado,
        Aceptado,
        Rechazado
    }
    public class ConcursoProveedor : Entity
    {
        public int ProveedorId { get; set; }
        public int ConcursoId { get; set; }
        public ProveedorConcursoStatusEnum Status { get; set; }
        public Proveedor Proveedor { get; set; }
        public bool AceptoTerminosYCondiciones { get; set; }
        public Oferta Oferta { get; set; }
    }
}