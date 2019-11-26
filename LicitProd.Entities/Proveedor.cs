namespace LicitProd.Entities
{
    public class Proveedor : Entity
    {
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string RazonSocial { get; set; }
        public ProveedorStatus Status { get; set; }
        public string Telefono { get; set; }

        private int _usuarioId;
        public Usuario Usuario { get; set; } = new Usuario();
        public int UsuarioId
        {
            get
            {
                if (Usuario != null)
                    return Usuario.Id;
                return _usuarioId;
            }
            set => _usuarioId = value;
        }
    }
}
