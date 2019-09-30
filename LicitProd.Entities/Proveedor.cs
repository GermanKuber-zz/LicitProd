namespace LicitProd.Entities
{
    public class Proveedor : Entity
    {
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string RazonSocial { get; set; }
        public ProveedorStatus Status { get; set; }
        public string Telefono { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        public void SetUsuario(Usuario usuario) => Usuario = usuario;
        public int UsuarioId
        {
            get { return Usuario.Id; }
            set
            {
                var a = "";
            }
        }


    }
}
