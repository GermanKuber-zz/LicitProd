namespace LicitProd.Entities
{
    public enum ProveedorStatus
    {
    }

    public class Proveedor : Entity
    {
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string RazonSocial { get; set; }
        public ProveedorStatus Status { get; set; }
        public string Telefono { get; set; }
    }
}
