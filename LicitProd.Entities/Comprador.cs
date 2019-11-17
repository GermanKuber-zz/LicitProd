namespace LicitProd.Entities
{
    public class Comprador : Entity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
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