using LicitProd.Entities;

namespace LicitProd.Seguridad
{

  

    public sealed class IdentityServices
    {
        private readonly static IdentityServices _instance = new IdentityServices();

        private static Usuario _usuarioLogueado;
        public static IdentityServices Instance => _instance;

        private IdentityServices()
        {

        }
        public void SetUserLogged(Usuario usuario)
        {
            _usuarioLogueado = usuario;
        }
        public void RemoveUserLogged()
        {
            _usuarioLogueado = null;
        }
        public Response<Usuario> IsLoggued() {
            if (_usuarioLogueado == null)
                return Response<Usuario>.Error();
            return Response<Usuario>.Ok(_usuarioLogueado);
        }
        public Usuario GetUserLogged() => _usuarioLogueado;
    }
}
