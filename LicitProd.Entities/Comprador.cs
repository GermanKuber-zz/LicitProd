namespace LicitProd.Entities
{
    public class Comprador : Entity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Usuario Usuario { get;   set; } = new Usuario();
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